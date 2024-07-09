using Expense_Tracker_dll;
using ExpenseTracker;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ExpenseTracker
{
    public partial class Form1 : Form
    {
        private int b_id;
        private List<Expense_Tracker_dll.Category> categories;
        private List<Budget> budgets;
        private DataTable expenses;
        private Salary salary;
        private User user;
        addexpense addexpense;
        ExpenseTrackerMain expenseTrackerMain;
        public Form1(User user, ExpenseTrackerMain expenseTrackerMain)
        {
            InitializeComponent();
            addexpense = new addexpense(this);
            this.user=user;
            this.expenseTrackerMain = expenseTrackerMain;
        }

        public void UpdateSalaryDisplay(double salary)
        {
            label29.Text = $"{salary:F2}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Open();
            guna2Button1.Enabled = false;
            guna2TabControl1.SelectedTab = tabPage8;
            guna2TextBox9.UseSystemPasswordChar = true;
            guna2DateTimePicker3.Enabled = false;
            guna2TabControl1.TabMenuVisible = false;
        }
        public List<Simulator_Expense> GetExpenses()
        {
            // Retrieve the current list of expenses from the data grid view or other source
            List<Simulator_Expense> expenses = new List<Simulator_Expense>();
            foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            {
                if (row.Cells[0].Value != null) // Ensure the row is not empty
                {
                    expenses.Add(new Simulator_Expense
                    {
                        Date = row.Cells[0].Value.ToString(),
                        Category = row.Cells[1].Value.ToString(),
                        Amount = Convert.ToDouble(row.Cells[2].Value),
                        Note = row.Cells[3].Value?.ToString()
                    });
                }
            }
            return expenses;
        }

        public void Profile_See()
        {
            if (user != null)
            {
                try
                {
                    guna2DateTimePicker3.Text = user.DateOfBirth;
                    guna2TextBox8.Text = user.Email;
                    guna2TextBox9.Text = user.Password;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("User not logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Open()
        {
            // Ensure the user object is initialized
            if (user == null)
            {
                MessageBox.Show("User not logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Clear existing items from combo boxes and DataGridView
            guna2ComboBox1.Items.Clear();
            guna2ComboBox4.Items.Clear();
            addexpense.guna2ComboBox1.Items.Clear();
            guna2DataGridView3.Rows.Clear();
            guna2DataGridView1.Rows.Clear();

            // Retrieve user salary and set label36
            try
            {
                salary = new Salary();
                double userSalary = salary.Retrieve(user.UserId);
                label36.Text = userSalary.ToString();

                // Retrieve categories and add them to the combo box
                Expense_Tracker_dll.Category category = new Expense_Tracker_dll.Category();
                categories = category.Retrieve(user.UserId);
                if (categories != null)
                {
                    foreach (Expense_Tracker_dll.Category cat in categories)
                    {
                        guna2ComboBox1.Items.Add(cat.Cate);
                        addexpense.guna2ComboBox1.Items.Add(cat.Cate);
                    }
                }

                // Retrieve and sum up total budget amount
                Budget budget = new Budget();
                double totalBudgetAmount = 0;
                budgets = budget.Retrieve(user.UserId);
                if (budgets != null)
                {
                    foreach (Budget bud in budgets)
                    {
                        guna2ComboBox4.Items.Add($"{bud.Type} - {bud.Amount}");
                        guna2DataGridView3.Rows.Add(bud.Type, bud.Amount);
                        totalBudgetAmount += bud.Amount;
                    }
                }

                // Deduct total budget amount from total salary and set label37
                double remainingAmount =  totalBudgetAmount - userSalary;
                label37.Text = remainingAmount.ToString("f2");
                label39.Text = totalBudgetAmount.ToString("f2");
                // Retrieve and populate expenses in DataGridView
                Expense expense = new Expense();
                expenses = expense.RetrieveExpensesWithType(user.UserId);
                if (expenses != null)
                {
                    foreach (DataRow row in expenses.Rows)
                    {
                        int rowIndex = guna2DataGridView1.Rows.Add();
                        guna2DataGridView1.Rows[rowIndex].Cells["Column1"].Value = row["e_id"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Column2"].Value = row["date"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Column6"].Value = row["time"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Column3"].Value = row["category"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Column4"].Value = row["amount"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Column5"].Value = row["note"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Column9"].Value = row["type"];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string filterText = guna2TextBox1.Text;
            if (!string.IsNullOrEmpty(filterText))
            {
                DataView dv = expenses.DefaultView;
                dv.RowFilter = $"category LIKE '%{filterText}%'";
                DataTable filteredTable = dv.ToTable();

                // Clear existing rows in the DataGridView
                guna2DataGridView1.Rows.Clear();

                // Add filtered rows to the DataGridView
                foreach (DataRow row in filteredTable.Rows)
                {
                    int rowIndex = guna2DataGridView1.Rows.Add();
                    guna2DataGridView1.Rows[rowIndex].Cells["Column1"].Value = row["e_id"];
                    guna2DataGridView1.Rows[rowIndex].Cells["Column2"].Value = row["date"];
                    guna2DataGridView1.Rows[rowIndex].Cells["Column6"].Value = row["time"];
                    guna2DataGridView1.Rows[rowIndex].Cells["Column3"].Value = row["category"];
                    guna2DataGridView1.Rows[rowIndex].Cells["Column4"].Value = row["amount"];
                    guna2DataGridView1.Rows[rowIndex].Cells["Column5"].Value = row["note"];
                    guna2DataGridView1.Rows[rowIndex].Cells["Column9"].Value = row["type"];
                }
            }
            else
            {
                this.Open();
            
            }
        }

        private void guna2Button21_Click(object sender, EventArgs e)
        {
            label28.Text = "00.0";
            label29.Text = "00.0";
            guna2DataGridView2.Rows.Clear();
            guna2Button19.Enabled = true;
            guna2TextBox7.ReadOnly = false;
            chart1.Series.Clear();
            chart2.Series.Clear();
        }
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (user != null)
            {
                try
                {
                    // Parse the amount from the textbox
                    double amount = 0.0;
                    if (double.TryParse(guna2TextBox2.Text, out amount))
                    {
                        // Get the selected budget type from the ComboBox
                        string selectedBudgetType = guna2ComboBox4.Text.Split('-')[0].Trim();

                        // Retrieve the budget amount based on the selected budget type
                        double budgetAmount = GetBudgetAmount(selectedBudgetType);

                        // Check if the expense amount exceeds the budget amount
                        if (amount > budgetAmount)
                        {
                            MessageBox.Show("Your expense amount exceeds your budget amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Get the current time in the desired format
                        string currentTime = DateTime.Now.ToString("h:mm tt"); // Format: 8:36 PM

                        // Create a new expense object
                        Expense expense = new Expense
                        {
                            UId = user.UserId,
                            BId = this.b_id, // Replace with the budget ID if applicable
                            Time = currentTime,
                            Date = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"), // Assuming you want to store date in this format
                            Category = guna2ComboBox1.Text,
                            Amount = amount,
                            Note = guna2TextBox3.Text
                        };

                        // Add the expense
                        string message = expense.Add(); // Assuming you have an Add method in your Expense class
                        if (message == "Expense added successfully.")
                        {
                            MessageBox.Show("Expense added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Reset controls
                            guna2TextBox2.Text = "";
                            guna2ComboBox1.SelectedIndex = -1; // Reset combobox selection
                            guna2DateTimePicker1.Value = DateTime.Now; // Reset datetimepicker
                            guna2TextBox3.Text = "";
                            this.Open();
                            // Update any other UI elements or refresh data if needed
                        }
                        else
                        {
                            MessageBox.Show($"Failed to add expense. Error: {message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid amount entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("User not logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to retrieve budget amount based on budget type
        private double GetBudgetAmount(string budgetType)
        {
            // Loop through budgets list to find the budget with matching type
            foreach (Budget bud in budgets)
            {
                if (bud.Type.Trim().Equals(budgetType, StringComparison.OrdinalIgnoreCase))
                {
                    return bud.Amount;
                }
            }
            return 0.0; // Return 0 if budget type is not found
        }
        private void guna2Button13_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (user == null)
            {
                MessageBox.Show("User not logged in or user data not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double amount = 0.0;
            if (!double.TryParse(guna2TextBox5.Text, out amount) || amount == 0)
            {
                MessageBox.Show("Please enter a valid non-zero amount for the budget.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Salary salary = new Salary();
            double userSalary = salary.Retrieve(user.UserId);

            // Check if the budget amount exceeds the user's salary
            if (amount > userSalary)
            {
                MessageBox.Show("Your budget amount exceeds your salary amount. Please update your salary.", "Budget Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Budget budget = new Budget
            {
                UserId = user.UserId,
                Amount = amount,
                Type = guna2ComboBox3.Text,
                Date = guna2ComboBox2.Text,
                Note = guna2TextBox6.Text
            };

            string message = budget.Add();
            if (message == "insertsuccess")
            {
                this.Open();
                MessageBox.Show("Budget Created successfully.", "Budget Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Reset controls
                guna2TextBox5.Text = "";
                guna2ComboBox3.SelectedIndex = -1; // Reset combobox selection
                guna2ComboBox2.SelectedIndex = -1; // Reset combobox selection
                guna2TextBox6.Text = "";
            }
            else if (message == "updatesuccess")
            {
                this.Open();
                MessageBox.Show("Budget updated successfully.", "Budget Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Reset controls
                guna2TextBox5.Text = "";
                guna2ComboBox3.SelectedIndex = -1; // Reset combobox selection
                guna2ComboBox2.SelectedIndex = -1; // Reset combobox selection
                guna2TextBox6.Text = "";
            }
            else
            {
                MessageBox.Show(message, "Budget Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void guna2Button11_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                double amount = 0.0;
                if (double.TryParse(amountsalrytextbox.Text, out amount))
                {
                    if (amount == 0)
                    {
                        MessageBox.Show("Please enter a non-zero amount for the salary.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Salary salary = new Salary
                    {
                        UserId = this.user.UserId,
                        Date = guna2DateTimePicker2.Value.ToLongDateString(),
                        SalaryAmount = amount,
                        Note = guna2TextBox4.Text
                    };

                    string message = salary.Add();
                    if (message == "insertsuccess")
                    {
                        MessageBox.Show("Salary added successfully.", "Salary Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Reset controls
                        amountsalrytextbox.Text = "";
                        guna2DateTimePicker2.Value = DateTime.Now; // Reset datetimepicker
                        guna2TextBox4.Text = "";
                        this.Open();
                    }
                    else if (message == "updatesuccess")
                    {
                        MessageBox.Show("Salary updated successfully.", "Salary Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Reset controls
                        amountsalrytextbox.Text = "";
                        guna2DateTimePicker2.Value = DateTime.Now; // Reset datetimepicker
                        guna2TextBox4.Text = "";
                        this.Open();
                    }
                    else
                    {
                        MessageBox.Show($"{message}", "Salary Added", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid amount entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("User not logged in or user data not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchExpenseByText(string searchText)
        {
            try
            {
                // Clear existing rows from the DataGridView
                guna2DataGridView1.Rows.Clear();

                // Retrieve the DataTable from the form
                DataTable expensesTable = expenses; // Replace "yourDataTable" with the actual name of your DataTable

                // Filter the DataTable based on the search text
                DataRow[] filteredRows = expensesTable.Select($"Category LIKE '%{searchText}%'");

                // Check if any expenses were found
                if (filteredRows.Length > 0)
                {
                    // Populate the DataGridView with data from the filtered DataTable
                    foreach (DataRow row in filteredRows)
                    {
                        // Add a new row to the DataGridView
                        int rowIndex = guna2DataGridView1.Rows.Add();

                        // Populate each cell in the row with data from the corresponding DataTable row
                        guna2DataGridView1.Rows[rowIndex].Cells["EId"].Value = row["e_id"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Date"].Value = row["date"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Time"].Value = row["time"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Category"].Value = row["category"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Amount"].Value = row["amount"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Note"].Value = row["note"];
                        guna2DataGridView1.Rows[rowIndex].Cells["Type"].Value = row["type"];
                    }
                }
                else
                {
                    MessageBox.Show("No expenses found matching the search text.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching for expenses: " + ex.Message);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage1;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage2;

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage3;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage4;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage5;

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage6;
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage7;

        }

        private void guna2Button20_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage8;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Category category = new Category(user,this, expenseTrackerMain);
            if (category != null )
            {
                category.Show();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            this.expenseTrackerMain.Close();
        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected in the ComboBox
            if (guna2ComboBox4.SelectedIndex != -1)
            {
                // Get the selected item from the ComboBox
                string selectedBudget = guna2ComboBox4.SelectedItem.ToString();

                // Split the selected item to extract type and amount
                string[] budgetParts = selectedBudget.Split('-');

                // Trim any leading or trailing spaces
                string type = budgetParts[0].Trim();
                double amount = double.Parse(budgetParts[1].Trim());

                // Search for the budget with the matching type and amount
                foreach (Budget budget in budgets)
                {
                    if (budget.Type == type && budget.Amount == amount)
                    {
                        // Found the matching budget, get its b_id
                        int  bId = budget.BudgetId;
                        // Now you can use bId as needed
                        this.b_id = bId;
                        break; // Exit the loop after finding the first matching budget
                    }
                }
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == guna2DataGridView1.Columns["Column7"].Index) // Assuming Column7 is the delete button column
            {
                // Prompt the user for confirmation before deleting
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this expense?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Check if the user confirms the deletion
                if (dialogResult == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];

                    // Retrieve the value of e_id and amount from the selected row
                    object eIdValue = selectedRow.Cells["Column1"].Value;
                    object amountValue = selectedRow.Cells["Column4"].Value; // Assuming Column4 is the amount column
                    int e_id;

                    // Check if the values are not null and can be converted to appropriate types
                    if (eIdValue != null && amountValue != null && int.TryParse(eIdValue.ToString(), out e_id) && double.TryParse(amountValue.ToString(), out double amount))
                    {
                        Expense expense = new Expense();
                        // Perform the deletion and update budget operation using the e_id and amount
                        string result = expense.DeleteAndUpdateBudget(e_id, amount);

                        // Check the result and display appropriate message
                        if (result == "Expense deleted and budget updated successfully.")
                        {
                            MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Open(); // Refresh the data
                        }
                        else
                        {
                            MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid expense ID or amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            double amount = 0.0;
            if (double.TryParse(guna2TextBox7.Text, out amount)) {
                guna2TextBox7.ReadOnly = true;
                addexpense.Salary = amount;
                guna2Button1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Invalid  amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            double amount = 0.0;
            if(double.TryParse(guna2TextBox7.Text,out amount)){

                label28.Text = amount.ToString("F2");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
             addexpense.ShowDialog();
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            // Set UseSystemPasswordChar to false when button is clicked
            guna2TextBox9.UseSystemPasswordChar = false;

            // Create and start the timer
            Timer timer = new Timer();
            timer.Interval = 5000; // 5000 milliseconds = 5 seconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            Timer timer = (Timer)sender;
            timer.Stop();

            // Set UseSystemPasswordChar to true after 5 seconds
            guna2TextBox9.UseSystemPasswordChar = true;
        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                try
                {
                    User user_upd = new User();
                    user_upd.DateOfBirth = guna2DateTimePicker3.Text;
                    user_upd.Email = guna2TextBox8.Text;
                    user_upd.Password = guna2TextBox9.Text;
                    user_upd.UserId = user.UserId;
                    // Call the UpdateUser method
                    string message = user_upd.Update();

                    // Handle the response
                    if (message == "Userupdated")
                    {
                        MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        guna2DateTimePicker3.Enabled = false;
                        guna2TextBox9.UseSystemPasswordChar = true;
                        guna2Button18.Visible = false;
                        guna2TextBox8.ReadOnly = true;
                        guna2TextBox9.ReadOnly = true;


                    }
                    else
                    {
                        MessageBox.Show($"Failed to update user. Error: {message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("User not logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
    }
    private void guna2Button8_Click(object sender, EventArgs e)
        {
            guna2TextBox8.ReadOnly = false;
            guna2TextBox9.ReadOnly = false;
            guna2Button18.Visible = true;
            guna2DateTimePicker3.Enabled = true;
            guna2TextBox9.UseSystemPasswordChar=false;
        }

        private void guna2Button17_Click(object sender, EventArgs e)
        {
            // Perform logout operation
            // For example, navigate to the login form or perform any necessary cleanup

            // Destroy or reset the user object
            user = null; // Assuming 'user' is the variable holding the user object

            // Assuming you have a LoginForm class where users can log in
            ExpenseTrackerMain loginForm = new ExpenseTrackerMain();
            loginForm.Show();

            // Close the current form (dashboard form)
            this.Close();
        }
    }
}

using Expense_Tracker_dll;
using ExpenseTracker;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenseTracker
{
    public partial class addexpense : Form
    {
        public double Salary { get; set; }
        private Form1 form;

        public addexpense(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            try
            {
                double amount = Convert.ToDouble(guna2TextBox2.Text);
                if (amount > Salary)
                {
                    MessageBox.Show("Insufficient funds.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Simulator_Expense expense = new Simulator_Expense
                {
                    Salary = Salary,
                    Date = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"),
                    Category = guna2ComboBox1.Text,
                    Amount = amount,
                    Note = guna2TextBox3.Text
                };

                expense.AddExpense(form);

                Salary -= amount;
                form.UpdateSalaryDisplay(Salary);

                List<Simulator_Expense> expenses = form.GetExpenses();
                expense.UpdateChartValues(form, expenses);

                // Reset controls
                guna2TextBox2.Text = "";
                guna2ComboBox1.SelectedIndex = -1; // Reset combobox selection
                guna2DateTimePicker1.Value = DateTime.Now; // Reset datetimepicker
                guna2TextBox3.Text = "";
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid amount entered. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


public class Simulator_Expense
{
    public double Salary { get; set; }
    public string Date { get; set; }
    public string Category { get; set; }
    public double Amount { get; set; }
    public string Note { get; set; }

    public void AddExpense(Form1 form)
    {
        form.guna2DataGridView2.Rows.Add(Date, Category, Amount, Note);
    }

    public void UpdateChartValues(Form1 form, List<Simulator_Expense> expenses)
    {
        UpdateBarChart(form, expenses);
        UpdatePieChart(form, expenses);
    }
    public void ClearDataAndResetCharts(Form1 form)
    {
        // Clear internal data if necessary (not shown in original code)

        // Clear the data grid view
        form.guna2DataGridView2.Rows.Clear();

        // Reset charts
        form.chart1.Series.Clear();
        form.chart2.Series.Clear();
    }
    private void UpdateBarChart(Form1 form, List<Simulator_Expense> expenses)
    {
        form.chart1.Series.Clear();

        int intervals = 10;
        double intervalSize = Salary / intervals;

        // Create a dictionary to hold interval data by category
        var intervalExpenses = new Dictionary<string, Dictionary<string, double>>();
        for (int i = 0; i < intervals; i++)
        {
            double lowerBound = i * intervalSize;
            double upperBound = (i + 1) * intervalSize;
            intervalExpenses.Add($"{lowerBound:F2} - {upperBound:F2}", new Dictionary<string, double>());
        }

        // Group expenses into intervals and categories
        foreach (var exp in expenses)
        {
            foreach (var interval in intervalExpenses.Keys.ToList())
            {
                var bounds = interval.Split('-').Select(double.Parse).ToArray();
                if (exp.Amount > bounds[0] && exp.Amount <= bounds[1])
                {
                    if (!intervalExpenses[interval].ContainsKey(exp.Category))
                    {
                        intervalExpenses[interval][exp.Category] = 0;
                    }
                    intervalExpenses[interval][exp.Category] += exp.Amount;
                    break;
                }
            }
        }

        // Create series for each category
        var categories = expenses.Select(e => e.Category).Distinct().ToList();
        foreach (var category in categories)
        {
            var series = new Series(category)
            {
                ChartType = SeriesChartType.Column
            };
            form.chart1.Series.Add(series);
        }

        // Add the interval data to the series
        foreach (var interval in intervalExpenses)
        {
            foreach (var category in interval.Value)
            {
                form.chart1.Series[category.Key].Points.AddXY(interval.Key, category.Value);
            }
        }

        // Update chart axis labels and titles
        form.chart1.ChartAreas[0].AxisX.Title = "Salary Intervals";
        form.chart1.ChartAreas[0].AxisY.Title = "Expense Amount";
    }

    private void UpdatePieChart(Form1 form, List<Simulator_Expense> expenses)
    {
        form.chart2.Series.Clear();

        Dictionary<string, double> categoryExpenses = expenses
            .GroupBy(exp => exp.Category)
            .ToDictionary(g => g.Key, g => g.Sum(exp => exp.Amount));

        Series series = new Series("Expense Categories")
        {
            ChartType = SeriesChartType.Pie
        };
        form.chart2.Series.Add(series);

        foreach (var category in categoryExpenses)
        {
            series.Points.AddXY(category.Key, category.Value);
        }
    }
}

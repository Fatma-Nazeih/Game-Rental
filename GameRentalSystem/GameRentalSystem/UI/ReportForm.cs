using GameRentalSystem.BLL;
using System;
using System.Data; // Needed for DataTable
using System.Windows.Forms;
using System.Drawing; // Needed for Icon

namespace GameRentalSystem.UI
{
    public partial class ReportForm : Form
    {
        private readonly ReportBLL _reportBLL = new ReportBLL();

        public ReportForm()
        {
            InitializeComponent(); // This calls the code in ReportForm.Designer.cs
            PopulateReportTypes(); // Populate the report type dropdown on form load
        }

        // Populates the cmbReportType dropdown with available report options
        private void PopulateReportTypes()
        {
            // Clear existing items to prevent duplicates if called multiple times
            cmbReportType.Items.Clear();
            // Add report options - ensure these strings match the cases in btnGenerateReport_Click
            cmbReportType.Items.Add("Most Rented Game"); // Report a
            cmbReportType.Items.Add("Games with No Renters Last Month"); // Report b
            cmbReportType.Items.Add("Client with Maximum Rentals Last Month"); // Report c
            cmbReportType.Items.Add("Vendor with Maximum Rentals Last Month"); // Report d
            cmbReportType.Items.Add("Vendors with No Rentals Last Month"); // Report e
            cmbReportType.Items.Add("Vendors Who Did Not Add Games Last Year"); // Report f

            cmbReportType.SelectedIndex = -1; // No default selection
        }

        // Linked to the btnGenerateReport.Click event in the designer
        // Generates and displays the selected report
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            // Get the selected report type from the dropdown
            string selectedReport = cmbReportType.SelectedItem?.ToString();

            // Validate that a report type has been selected
            if (string.IsNullOrWhiteSpace(selectedReport))
            {
                MessageBox.Show("Please select a report type.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvReportResults.DataSource = null; // Clear any previously displayed results
                return; // Stop the report generation process
            }

            try
            {
                DataTable reportData = null;

                // Use a switch statement to call the appropriate BLL method based on the selected report
                switch (selectedReport)
                {
                    case "Most Rented Game":
                        reportData = _reportBLL.GetMostRentedGameReport(); // Call Report a BLL method
                        break;
                    case "Games with No Renters Last Month":
                        reportData = _reportBLL.GetGamesWithNoRentersLastMonth(); // Call Report b BLL method
                        break;
                    case "Client with Maximum Rentals Last Month":
                        reportData = _reportBLL.GetClientWithMaxRentalsLastMonth(); // Call Report c BLL method
                        break;
                    case "Vendor with Maximum Rentals Last Month":
                        reportData = _reportBLL.GetVendorWithMaxRentalsLastMonth(); // Call Report d BLL method
                        break;
                    case "Vendors with No Rentals Last Month":
                        reportData = _reportBLL.GetVendorsWithNoRentalsLastMonth(); // Call Report e BLL method
                        break;
                    case "Vendors Who Did Not Add Games Last Year":
                        reportData = _reportBLL.GetVendorsWhoDidNotAddGamesLastYear(); // Call Report f BLL method
                        break;
                    default:
                        // Should not be reached if PopulateReportTypes matches this switch
                        MessageBox.Show("Selected report type is not implemented.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvReportResults.DataSource = null; // Clear results
                        return; // Stop the process
                }

                // Bind the generated report data to the DataGridView
                if (reportData != null)
                {
                    dgvReportResults.DataSource = reportData;

                    // Display a message if the report returned no data
                    if (reportData.Rows.Count == 0)
                    {
                        MessageBox.Show($"No data found for '{selectedReport}'.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Optional: Auto-size columns for better readability
                    dgvReportResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (in a real application)
                Console.WriteLine($"Error generating report '{selectedReport}': " + ex.Message);
                // Display a user-friendly error message
                MessageBox.Show($"Error generating report '{selectedReport}': " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Optional: Add a button or menu item to close the form
        // private void btnClose_Click(object sender, EventArgs e)
        // {
        //     this.Close();
        // }
    }
}
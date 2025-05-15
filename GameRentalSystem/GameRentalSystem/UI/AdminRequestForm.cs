using GameRentalSystem.BLL;
using GameRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing; // Needed for Icon

namespace GameRentalSystem.UI
{
    public partial class AdminRequestForm : Form
    {
        private readonly User _currentAdminUser;
        private readonly AdminBLL _adminBLL = new AdminBLL();

        public AdminRequestForm(User adminUser)
        {
            InitializeComponent(); // This calls the code in AdminRequestForm.Designer.cs
            _currentAdminUser = adminUser;
            LoadPendingRequests();
        }

        private void LoadPendingRequests()
        {
            try
            {
                List<AdminRequest> requests = _adminBLL.GetPendingAdminRequests();
                dgvAdminRequests.DataSource = requests;
                // Optional: Customize columns
                if (dgvAdminRequests.Columns.Contains("RequestID")) dgvAdminRequests.Columns["RequestID"].Visible = false;
                if (dgvAdminRequests.Columns.Contains("ClientID")) dgvAdminRequests.Columns["ClientID"].Visible = false;
                if (dgvAdminRequests.Columns.Contains("AdminApproverID")) dgvAdminRequests.Columns["AdminApproverID"].Visible = false; // Should be null for pending
                if (dgvAdminRequests.Columns.Contains("ApprovalDate")) dgvAdminRequests.Columns["ApprovalDate"].Visible = false; // Should be null for pending

                // Auto-size columns for better readability
                dgvAdminRequests.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error loading admin requests: " + ex.Message);
                MessageBox.Show("Error loading admin requests: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Linked to btnApprove.Click event in designer
        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvAdminRequests.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvAdminRequests.SelectedRows[0];
                // Ensure RequestID and ClientUsername cells exist and contain valid data
                if (selectedRow.Cells["RequestID"] == null || selectedRow.Cells["RequestID"].Value == null || !(selectedRow.Cells["RequestID"].Value is int) ||
                    selectedRow.Cells["ClientUsername"] == null || selectedRow.Cells["ClientUsername"].Value == null || !(selectedRow.Cells["ClientUsername"].Value is string))
                {
                    MessageBox.Show("Could not get request details from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int requestId = (int)selectedRow.Cells["RequestID"].Value;
                string clientUsername = selectedRow.Cells["ClientUsername"].Value.ToString();
                // Optionally check status here, but BLL method also checks

                DialogResult confirm = MessageBox.Show($"Approve admin request from '{clientUsername}'?",
                                                     "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Process the request (approve = true) using BLL
                        bool success = _adminBLL.ProcessAdminRequest(requestId, _currentAdminUser.UserID, true); // true for approve

                        if (success)
                        {
                            MessageBox.Show($"Request from '{clientUsername}' approved. User role updated to Admin.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPendingRequests(); // Refresh requests list
                                                   // Note: You might want to also notify or refresh the user list in the AdminMainForm.
                                                   // This requires communication between forms (e.g., events or passing references).
                        }
                        else
                        {
                            MessageBox.Show($"Failed to approve request from '{clientUsername}'. It might have been processed already or an error occurred.", "Approval Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine("Error processing request: " + ex.Message);
                        MessageBox.Show("Error processing request: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a request to approve.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Linked to btnReject.Click event in designer
        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvAdminRequests.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvAdminRequests.SelectedRows[0];
                // Ensure RequestID and ClientUsername cells exist and contain valid data
                if (selectedRow.Cells["RequestID"] == null || selectedRow.Cells["RequestID"].Value == null || !(selectedRow.Cells["RequestID"].Value is int) ||
                    selectedRow.Cells["ClientUsername"] == null || selectedRow.Cells["ClientUsername"].Value == null || !(selectedRow.Cells["ClientUsername"].Value is string))
                {
                    MessageBox.Show("Could not get request details from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int requestId = (int)selectedRow.Cells["RequestID"].Value;
                string clientUsername = selectedRow.Cells["ClientUsername"].Value.ToString();
                // Optionally check status here, but BLL method also checks


                DialogResult confirm = MessageBox.Show($"Reject admin request from '{clientUsername}'?",
                                                     "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Process the request (approve = false) using BLL
                        bool success = _adminBLL.ProcessAdminRequest(requestId, _currentAdminUser.UserID, false); // false for reject

                        if (success)
                        {
                            MessageBox.Show($"Request from '{clientUsername}' rejected.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPendingRequests(); // Refresh requests list
                                                   // Optional: Delete rejected requests after rejection (if desired and implemented in BLL/DAL)
                                                   // _adminBLL.DeleteRejectedRequest(requestId); // Needs BLL wrapper

                        }
                        else
                        {
                            MessageBox.Show($"Failed to reject request from '{clientUsername}'. It might have been processed already or an error occurred.", "Rejection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine("Error processing request: " + ex.Message);
                        MessageBox.Show("Error processing request: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a request to reject.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Linked to btnRefresh.Click event in designer
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPendingRequests();
        }

        // Optional: Handle form closing if this form is shown independently
        // private void AdminRequestForm_FormClosing(object sender, FormClosingEventArgs e) { ... }
    }
}
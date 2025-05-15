using GameRentalSystem.BLL;
using GameRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Required for Interaction.InputBox

namespace GameRentalSystem.UI
{
    public partial class AdminMainForm : Form
    {
        private readonly User _currentAdminUser;
        private readonly GameBLL _gameBLL = new GameBLL();
        private readonly AdminBLL _adminBLL = new AdminBLL();
        private readonly ReportBLL _reportBLL = new ReportBLL();

        public AdminMainForm(User adminUser)
        {
            InitializeComponent(); // This calls the code in AdminMainForm.Designer.cs
            _currentAdminUser = adminUser;
            lblWelcome.Text = $"Welcome, Admin {_currentAdminUser.FullName}!";

            // Load initial data when the form opens
            LoadUsers();
            LoadAllGames();
            LoadPendingAdminRequests();
            PopulateReportTypes();
        }

        // --- Data Loading Methods ---

        private void LoadUsers()
        {
            try
            {
                List<User> users = _adminBLL.GetAllUsers();
                // Bind list directly to DataGridView
                dgvUsers.DataSource = users;
                // Optional: Customize columns (hide Password, show specific columns)
                if (dgvUsers.Columns.Contains("Password"))
                {
                    dgvUsers.Columns["Password"].Visible = false; // Assuming Password is in the User model (should be hash)
                }


            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error loading users: " + ex.Message);
                MessageBox.Show("Error loading users: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllGames()
        {
            try
            {
                List<Game> games = _gameBLL.GetAllGamesForAdmin();
                // Bind list directly to DataGridView
                dgvGames.DataSource = games;
                // Optional: Customize columns
                if (dgvGames.Columns.Contains("GameID")) dgvGames.Columns["GameID"].Visible = false;
                if (dgvGames.Columns.Contains("VendorID")) dgvGames.Columns["VendorID"].Visible = false;
                if (dgvGames.Columns.Contains("CategoryID")) dgvGames.Columns["CategoryID"].Visible = false;
                if (dgvGames.Columns.Contains("AdminApproverID")) dgvGames.Columns["AdminApproverID"].Visible = false;
                if (dgvGames.Columns.Contains("ApprovalDate")) dgvGames.Columns["ApprovalDate"].Visible = false;


                // Format Price column if it exists
                if (dgvGames.Columns.Contains("Price"))
                {
                    dgvGames.Columns["Price"].DefaultCellStyle.Format = "C2"; // Currency format
                }

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error loading games: " + ex.Message);
                MessageBox.Show("Error loading games: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPendingAdminRequests()
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

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error loading admin requests: " + ex.Message);
                MessageBox.Show("Error loading admin requests: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateReportTypes()
        {
            // Add report options to the ComboBox - ensure these match cases in btnGenerateReport_Click
            cmbReportType.Items.Clear(); // Clear existing items
            cmbReportType.Items.Add("Most Rented Game"); // Report a
            cmbReportType.Items.Add("Games with No Renters Last Month"); // Report b
            cmbReportType.Items.Add("Client with Maximum Rentals Last Month"); // Report c
            cmbReportType.Items.Add("Vendor with Maximum Rentals Last Month"); // Report d
            cmbReportType.Items.Add("Vendors with No Rentals Last Month"); // Report e
            cmbReportType.Items.Add("Vendors Who Did Not Add Games Last Year"); // Report f

            cmbReportType.SelectedIndex = -1; // No default selection
        }


        // --- User Management ---

        // Linked to btnChangeRole.Click event in designer
        private void btnChangeRole_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];
                // Ensure the UserID cell exists and contains a valid integer
                if (selectedRow.Cells["UserID"] == null || selectedRow.Cells["UserID"].Value == null || !(selectedRow.Cells["UserID"].Value is int))
                {
                    MessageBox.Show("Could not get User ID from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int userId = (int)selectedRow.Cells["UserID"].Value;

                // Ensure the Role cell exists and contains a string
                if (selectedRow.Cells["Role"] == null || selectedRow.Cells["Role"].Value == null || !(selectedRow.Cells["Role"].Value is string))
                {
                    MessageBox.Show("Could not get User Role from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string currentRole = selectedRow.Cells["Role"].Value.ToString();

                // Ensure the Username cell exists and contains a string
                if (selectedRow.Cells["Username"] == null || selectedRow.Cells["Username"].Value == null || !(selectedRow.Cells["Username"].Value is string))
                {
                    MessageBox.Show("Could not get Username from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string username = selectedRow.Cells["Username"].Value.ToString();


                // Prevent changing the role of another Admin or yourself
                if (currentRole == "Admin")
                {
                    if (userId == _currentAdminUser.UserID)
                    {
                        MessageBox.Show("You cannot change your own role directly here.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Cannot change the role of another Admin directly here.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }

                // Simple dialog to get new role (Client or Vendor)
                string prompt = $"Enter new role for '{username}' (Client or Vendor):";
                string dialogTitle = "Change User Role";
                string defaultValue = currentRole; // Default to current role
                string newRoleInput = Interaction.InputBox(prompt, dialogTitle, defaultValue); // Requires reference to Microsoft.VisualBasic

                if (string.IsNullOrWhiteSpace(newRoleInput) || (newRoleInput != "Client" && newRoleInput != "Vendor"))
                {
                    MessageBox.Show("Invalid role entered. Please enter 'Client' or 'Vendor'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string newRole = newRoleInput;

                if (newRole == currentRole)
                {
                    MessageBox.Show("Role is already set to that value.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    // Call BLL to update the user's role
                    bool success = _adminBLL.UpdateUserRole(userId, newRole);
                    if (success)
                    {
                        MessageBox.Show($"Successfully changed role for '{username}' to '{newRole}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers(); // Refresh user list to show the change
                    }
                    else
                    {
                        MessageBox.Show($"Failed to change role for '{username}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine("Error changing user role: " + ex.Message);
                    MessageBox.Show("Error changing user role: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Please select a user to change their role.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Linked to btnDeleteUser.Click event in designer
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];
                // Ensure UserID, Username, and Role cells exist and contain valid data
                if (selectedRow.Cells["UserID"] == null || selectedRow.Cells["UserID"].Value == null || !(selectedRow.Cells["UserID"].Value is int) ||
                    selectedRow.Cells["Username"] == null || selectedRow.Cells["Username"].Value == null || !(selectedRow.Cells["Username"].Value is string) ||
                    selectedRow.Cells["Role"] == null || selectedRow.Cells["Role"].Value == null || !(selectedRow.Cells["Role"].Value is string))
                {
                    MessageBox.Show("Could not get user details from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int userId = (int)selectedRow.Cells["UserID"].Value;
                string username = selectedRow.Cells["Username"].Value.ToString();
                string role = selectedRow.Cells["Role"].Value.ToString();

                // Prevent deleting your own admin account or other admin accounts
                if (role == "Admin")
                {
                    if (userId == _currentAdminUser.UserID)
                    {
                        MessageBox.Show("You cannot delete your own admin account.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete another Admin account.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }


                DialogResult confirm = MessageBox.Show($"Are you sure you want to delete user '{username}'?\n" +
                                                     "WARNING: This action cannot be undone and may fail if the user has associated data (rentals, games).",
                                                     "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Call BLL to delete the user. The BLL/DAL should handle checks
                        // for associated data (rentals, games if vendor) before deleting.
                        bool success = _adminBLL.DeleteUser(userId);

                        if (success)
                        {
                            MessageBox.Show($"User '{username}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadUsers(); // Refresh user list
                        }
                        else
                        {
                            // The BLL/DAL should ideally provide a more specific reason for failure
                            MessageBox.Show($"Failed to delete user '{username}'.\nReason: User may have associated data (rentals, games) that prevent deletion.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine("Error deleting user: " + ex.Message);
                        MessageBox.Show("Error deleting user: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // --- Game Management ---

        // Linked to btnAddGame.Click event in designer
        private void btnAddGame_Click(object sender, EventArgs e)
        {
            // Open the Add/Edit Game form in Add mode
            AddEditGameForm addGameForm = new AddEditGameForm(_currentAdminUser, null); // Pass null for Game to indicate add mode
            // ShowDialog makes the form modal, blocking interaction with the parent until it's closed
            DialogResult result = addGameForm.ShowDialog();

            // Refresh game list if the game was successfully added (DialogResult.OK)
            if (result == DialogResult.OK)
            {
                LoadAllGames();
            }
        }

        // Linked to btnEditGame.Click event in designer
        private void btnEditGame_Click(object sender, EventArgs e)
        {
            if (dgvGames.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvGames.SelectedRows[0];
                // Ensure GameID cell exists and contains a valid integer
                if (selectedRow.Cells["GameID"] == null || selectedRow.Cells["GameID"].Value == null || !(selectedRow.Cells["GameID"].Value is int))
                {
                    MessageBox.Show("Could not get Game ID from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int gameId = (int)selectedRow.Cells["GameID"].Value;

                try
                {
                    // Retrieve the full game details from the database using BLL
                    Game gameToEdit = _gameBLL.GetGameById(gameId);

                    if (gameToEdit != null)
                    {
                        // Open the Add/Edit Game form in Edit mode, passing the game object
                        AddEditGameForm editGameForm = new AddEditGameForm(_currentAdminUser, gameToEdit);
                        DialogResult result = editGameForm.ShowDialog();

                        // Refresh game list if the game was successfully updated
                        if (result == DialogResult.OK)
                        {
                            LoadAllGames();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not retrieve game details for editing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine("Error retrieving game for editing: " + ex.Message);
                    MessageBox.Show("Error retrieving game for editing: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a game to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Linked to btnDeleteGame.Click event in designer
        private void btnDeleteGame_Click(object sender, EventArgs e)
        {
            if (dgvGames.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvGames.SelectedRows[0];
                // Ensure GameID and Title cells exist and contain valid data
                if (selectedRow.Cells["GameID"] == null || selectedRow.Cells["GameID"].Value == null || !(selectedRow.Cells["GameID"].Value is int) ||
                    selectedRow.Cells["Title"] == null || selectedRow.Cells["Title"].Value == null || !(selectedRow.Cells["Title"].Value is string))
                {
                    MessageBox.Show("Could not get game details from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int gameId = (int)selectedRow.Cells["GameID"].Value;
                string gameTitle = selectedRow.Cells["Title"].Value.ToString();

                DialogResult confirm = MessageBox.Show($"Are you sure you want to delete the game '{gameTitle}'?\n" +
                                                     "WARNING: This action cannot be undone and may fail if there are active rentals for this game.",
                                                     "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Call BLL to delete the game. The BLL/DAL should handle the check for active rentals.
                        bool success = _gameBLL.DeleteGame(gameId);

                        if (success)
                        {
                            MessageBox.Show($"Game '{gameTitle}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAllGames(); // Refresh game list
                        }
                        else
                        {
                            // The BLL/DAL should ideally provide a more specific reason for failure
                            MessageBox.Show($"Failed to delete game '{gameTitle}'.\nReason: There are active rentals for this game.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine("Error deleting game: " + ex.Message);
                        MessageBox.Show("Error deleting game: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a game to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Linked to btnApproveGame.Click event in designer
        private void btnApproveGame_Click(object sender, EventArgs e)
        {
            if (dgvGames.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvGames.SelectedRows[0];
                // Ensure GameID, Title, and IsApproved cells exist and contain valid data
                if (selectedRow.Cells["GameID"] == null || selectedRow.Cells["GameID"].Value == null || !(selectedRow.Cells["GameID"].Value is int) ||
                    selectedRow.Cells["Title"] == null || selectedRow.Cells["Title"].Value == null || !(selectedRow.Cells["Title"].Value is string) ||
                    selectedRow.Cells["IsApproved"] == null || selectedRow.Cells["IsApproved"].Value == null || !(selectedRow.Cells["IsApproved"].Value is bool))
                {
                    MessageBox.Show("Could not get game details from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int gameId = (int)selectedRow.Cells["GameID"].Value;
                string gameTitle = selectedRow.Cells["Title"].Value.ToString();
                bool isApproved = (bool)selectedRow.Cells["IsApproved"].Value;

                if (isApproved)
                {
                    MessageBox.Show($"Game '{gameTitle}' is already approved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult confirm = MessageBox.Show($"Are you sure you want to approve the game '{gameTitle}'?",
                                                     "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Call BLL to approve the game
                        bool success = _gameBLL.ApproveGame(gameId, _currentAdminUser.UserID);

                        if (success)
                        {
                            MessageBox.Show($"Game '{gameTitle}' approved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAllGames(); // Refresh game list
                        }
                        else
                        {
                            // This might happen if the game was already approved by someone else between loading and clicking
                            MessageBox.Show($"Failed to approve game '{gameTitle}'. It might have been approved already or an error occurred.", "Approval Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine("Error approving game: " + ex.Message);
                        MessageBox.Show("Error approving game: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a game to approve.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Linked to btnRejectGame.Click event in designer (Optional, but good practice)
        private void btnRejectGame_Click(object sender, EventArgs e)
        {
            if (dgvGames.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvGames.SelectedRows[0];
                int gameId = (int)selectedRow.Cells["GameID"].Value;
                string gameTitle = selectedRow.Cells["Title"].Value.ToString();
                bool isApproved = (bool)selectedRow.Cells["IsApproved"].Value;

                // Optional Business Logic: Prevent rejecting a game that is already unapproved
                if (!isApproved)
                {
                    MessageBox.Show($"Game '{gameTitle}' is already pending or unapproved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Confirm the rejection action with the user
                DialogResult confirm = MessageBox.Show($"Are you sure you want to reject/unapprove the game '{gameTitle}'?",
                                                   "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Call the BLL method to reject the game
                        // Pass the gameId and the current admin user's ID
                        bool success = _gameBLL.RejectGame(gameId, _currentAdminUser.UserID); // Calls the NEW BLL method

                        if (success)
                        {
                            MessageBox.Show($"Game '{gameTitle}' rejected successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAllGames(); // Refresh the game list to show the updated status
                        }
                        else
                        {
                            // This might happen if the game was already rejected by someone else
                            // or if an error occurred in the DAL.
                            MessageBox.Show($"Failed to reject game '{gameTitle}'. It might have been processed already or an error occurred.", "Rejection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine("Error rejecting game: " + ex.Message);
                        MessageBox.Show("Error rejecting game: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Please select a game to reject.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // --- Admin Requests ---

        // Linked to btnApproveRequest.Click event in designer
        private void btnApproveRequest_Click(object sender, EventArgs e)
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
                            LoadPendingAdminRequests(); // Refresh requests list
                            LoadUsers(); // Refresh user list (role changed)
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

        // Linked to btnRejectRequest.Click event in designer
        private void btnRejectRequest_Click(object sender, EventArgs e)
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
                            LoadPendingAdminRequests(); // Refresh requests list
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

        // Linked to btnRefreshRequests.Click event in designer (Optional refresh button)
        private void btnRefreshRequests_Click(object sender, EventArgs e)
        {
            LoadPendingAdminRequests();
        }


        // --- Reports ---

        // Linked to btnGenerateReport.Click event in designer
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string selectedReport = cmbReportType.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(selectedReport))
            {
                MessageBox.Show("Please select a report type.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvReportResults.DataSource = null; // Clear previous results
                return;
            }

            try
            {
                DataTable reportData = null;

                // Call the appropriate ReportBLL method based on selected report type
                switch (selectedReport)
                {
                    case "Most Rented Game":
                        reportData = _reportBLL.GetMostRentedGameReport();
                        break;
                    case "Games with No Renters Last Month":
                        reportData = _reportBLL.GetGamesWithNoRentersLastMonth();
                        break;
                    case "Client with Maximum Rentals Last Month":
                        reportData = _reportBLL.GetClientWithMaxRentalsLastMonth();
                        break;
                    case "Vendor with Maximum Rentals Last Month":
                        reportData = _reportBLL.GetVendorWithMaxRentalsLastMonth();
                        break;
                    case "Vendors with No Rentals Last Month":
                        reportData = _reportBLL.GetVendorsWithNoRentalsLastMonth();
                        break;
                    case "Vendors Who Did Not Add Games Last Year":
                        reportData = _reportBLL.GetVendorsWhoDidNotAddGamesLastYear();
                        break;
                    default:
                        MessageBox.Show("Selected report type is not implemented.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvReportResults.DataSource = null;
                        return;
                }

                if (reportData != null)
                {
                    dgvReportResults.DataSource = reportData;
                    if (reportData.Rows.Count == 0)
                    {
                        MessageBox.Show($"No data found for '{selectedReport}'.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error generating report '{selectedReport}': " + ex.Message);
                MessageBox.Show($"Error generating report '{selectedReport}': " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Handle form closing (linked in designer)
        private void AdminMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm(); 
            loginForm.Show();
        }

        // Optional: Add Refresh buttons for grids (linked in designer)
        private void btnRefreshUsers_Click(object sender, EventArgs e) { LoadUsers(); }
        private void btnRefreshGames_Click(object sender, EventArgs e) { LoadAllGames(); }
        // btnRefreshRequests_Click is already implemented above

        private void button1_Click(object sender, EventArgs e)
        {
            // Confirm with the user before logging out (optional but good practice)
            DialogResult confirm = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // Hide the current form instead of closing it immediately
                this.Hide();
                LoginForm loginForm = new LoginForm(); // Create a new instance of the Login form
                loginForm.Show(); // Show the Login form
                
            }
        }


    }
}
using GameRentalSystem.BLL;
using GameRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data; // Needed for DataTable if converting list
using System.Windows.Forms;
using Microsoft.VisualBasic; // Needed for InputBox (requires adding reference)
using System.Drawing; // Needed for Icon if using

namespace GameRentalSystem.UI
{
    // Form for Admin users to manage other users (view, change role, delete)
    public partial class UserManagementForm : Form
    {
        private readonly User _currentAdminUser; // The logged-in admin user
        private readonly AdminBLL _adminBLL = new AdminBLL();

        // Constructor takes the current admin user
        public UserManagementForm(User adminUser)
        {
            InitializeComponent(); // This calls the code in UserManagementForm.Designer.cs
            _currentAdminUser = adminUser;

            // Load the list of users when the form opens
            LoadUsers();
        }

        // Loads all users from the database and displays them in the DataGridView
        private void LoadUsers()
        {
            try
            {
                // Get the list of all users from the BLL
                List<User> users = _adminBLL.GetAllUsers(); // Assuming GetAllUsers is implemented in AdminBLL/UserDAL

                // Bind the list of users directly to the DataGridView
                dgvUsers.DataSource = users;

                // Optional: Customize DataGridView columns
                if (dgvUsers.Columns.Contains("Password"))
                {
                    dgvUsers.Columns["Password"].Visible = false; // Hide the password (should be hash)
                }
                if (dgvUsers.Columns.Contains("UserID")) dgvUsers.Columns["UserID"].Visible = false; // Hide the ID column
                if (dgvUsers.Columns.Contains("RegistrationDate"))
                {
                    dgvUsers.Columns["RegistrationDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm"; // Example date format
                }

                // Auto-size columns for better readability
                dgvUsers.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
            catch (Exception ex)
            {
                // Log the exception (in a real application)
                Console.WriteLine("Error loading users: " + ex.Message);
                MessageBox.Show("Error loading users: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Linked to the btnChangeRole.Click event in the designer
        // Allows admin to change the role of a selected user
        private void btnChangeRole_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dgvUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];
                // Get the UserID, current Role, and Username from the selected row
                int userId = (int)selectedRow.Cells["UserID"].Value;
                string currentRole = selectedRow.Cells["Role"].Value.ToString();
                string username = selectedRow.Cells["Username"].Value.ToString();

                // Business Logic: Prevent changing the role of the currently logged-in admin
                if (userId == _currentAdminUser.UserID)
                {
                    MessageBox.Show("You cannot change your own role.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Business Logic: Prevent changing the role of other admins directly here
                // Admin role changes for others should ideally go through the AdminRequest process (Client -> Admin)
                // or a separate, more controlled process if Admin -> Client/Vendor is allowed.
                if (currentRole == "Admin")
                {
                    MessageBox.Show("Cannot change the role of another Admin directly here.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Simple dialog to get the new role (Allow changing Client <-> Vendor)
                string prompt = $"Enter new role for '{username}' (Client or Vendor):";
                string dialogTitle = "Change User Role";
                string defaultValue = currentRole; // Default to the user's current role
                                                   // Use InputBox from Microsoft.VisualBasic (requires adding reference)
                string newRoleInput = Interaction.InputBox(prompt, dialogTitle, defaultValue);

                // Validate the input role
                if (!string.IsNullOrWhiteSpace(newRoleInput) && (newRoleInput.Equals("Client", StringComparison.OrdinalIgnoreCase) || newRoleInput.Equals("Vendor", StringComparison.OrdinalIgnoreCase)))
                {
                    string newRole = newRoleInput; // Use the user's valid input

                    // Check if the new role is the same as the current role
                    if (newRole.Equals(currentRole, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Role is already set to that value.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return; // No change needed
                    }

                    try
                    {
                        // Call the BLL method to update the user's role
                        // The BLL method should handle the database update and potentially
                        // logic related to Vendor entries if changing to/from Vendor.
                        bool success = _adminBLL.UpdateUserRole(userId, newRole); // Assuming UpdateUserRole is implemented in AdminBLL/UserDAL

                        if (success)
                        {
                            MessageBox.Show($"Successfully changed role for '{username}' to '{newRole}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadUsers(); // Refresh the user list to show the updated role
                        }
                        else
                        {
                            // The BLL method returned false, indicating a failure (e.g., DB error)
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
                else if (!string.IsNullOrWhiteSpace(newRoleInput)) // User entered something, but it wasn't "Client" or "Vendor"
                {
                    MessageBox.Show("Invalid role entered. Please enter 'Client' or 'Vendor'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // If newRoleInput is empty, the user cancelled the input box, so do nothing.

            }
            else
            {
                MessageBox.Show("Please select a user to change their role.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Linked to the btnDeleteUser.Click event in the designer
        // Allows admin to delete a selected user
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dgvUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];
                // Get the UserID, Username, and Role from the selected row
                int userId = (int)selectedRow.Cells["UserID"].Value;
                string username = selectedRow.Cells["Username"].Value.ToString();
                string role = selectedRow.Cells["Role"].Value.ToString();

                // Business Logic: Prevent deleting the currently logged-in admin
                if (userId == _currentAdminUser.UserID)
                {
                    MessageBox.Show("You cannot delete your own admin account.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Business Logic: Prevent deleting other admins
                if (role == "Admin")
                {
                    MessageBox.Show("Cannot delete another Admin account.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Confirm the deletion action with the user
                DialogResult confirm = MessageBox.Show($"Are you sure you want to delete user '{username}'?\n" +
                                                     "WARNING: This may fail if there are associated games or rentals.",
                                                     "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Call the BLL method to delete the user.
                        // The BLL method should contain the business logic checks
                        // (e.g., active rentals, games owned by vendor, pending admin requests)
                        // and handle the database deletion or soft deletion.
                        bool success = _adminBLL.DeleteUser(userId); // Assuming DeleteUser is implemented in AdminBLL/UserDAL

                        if (success)
                        {
                            MessageBox.Show($"User '{username}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadUsers(); // Refresh the user list
                                         // If the deleted user was a Vendor, you might need to refresh the games list in AdminMainForm
                                         // If the deleted user had pending requests, refresh the requests list in AdminMainForm
                        }
                        else
                        {
                            // The BLL/DAL method returned false, likely due to associated data preventing deletion
                            MessageBox.Show($"Failed to delete user '{username}'.\nReason: User may have associated data (rentals, games, requests) that prevent deletion.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Optional: Add a refresh button for the user list (linked in designer)
        private void btnRefreshUsers_Click(object sender, EventArgs e)
        {
            LoadUsers(); // Reload the user list
        }

        // Optional: Handle FormClosing event if needed (e.g., if this form is modal and needs to update the parent)
        // private void UserManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        // {
        //     // If this form was shown with ShowDialog(), you might set DialogResult here
        //     // or trigger a refresh method on the parent form if you have a reference.
        // }
    }
}

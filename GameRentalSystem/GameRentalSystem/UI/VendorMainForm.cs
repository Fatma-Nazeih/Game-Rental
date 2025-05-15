using GameRentalSystem.BLL;
using GameRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data; // Needed for DataTable if converting list (though direct binding is often possible)
using System.Windows.Forms;
using System.Drawing; // Needed for Icon if using

namespace GameRentalSystem.UI
{
    public partial class VendorMainForm : Form
    {
        private readonly User _currentVendorUser;
        private readonly GameBLL _gameBLL = new GameBLL();
        // Assuming VendorBLL is needed here to get the VendorID from UserID
        private readonly VendorBLL _vendorBLL = new VendorBLL();


        public VendorMainForm(User vendorUser)
        {
            InitializeComponent(); // This calls the code in VendorMainForm.Designer.cs
            _currentVendorUser = vendorUser;
            lblWelcome.Text = $"Welcome, Vendor {_currentVendorUser.FullName}!";

            // Load games submitted by this vendor when the form opens
            LoadVendorGames();
        }

        // Loads games submitted by the current vendor into the DataGridView
        private void LoadVendorGames()
        {
            try
            {
                // Get the list of games for the current vendor from the BLL
                // The BLL method should handle getting the VendorID from the UserID
                List<Game> games = _gameBLL.GetGamesForVendor(_currentVendorUser.UserID); // Assuming GetGamesForVendor is implemented in GameBLL/DAL

                // Bind the list of games directly to the DataGridView
                dgvMyGames.DataSource = games;

                // Optional: Customize DataGridView columns
                if (dgvMyGames.Columns.Contains("GameID")) dgvMyGames.Columns["GameID"].Visible = false; // Hide the ID column
                if (dgvMyGames.Columns.Contains("VendorID")) dgvMyGames.Columns["VendorID"].Visible = false; // Already filtered by vendor
                if (dgvMyGames.Columns.Contains("CategoryID")) dgvMyGames.Columns["CategoryID"].Visible = false; // Can hide if Category Name is shown
                // Hide Admin-specific approval details from the vendor view
                if (dgvMyGames.Columns.Contains("AdminApproverID")) dgvMyGames.Columns["AdminApproverID"].Visible = false;
                if (dgvMyGames.Columns.Contains("ApprovalDate")) dgvMyGames.Columns["ApprovalDate"].Visible = false;

                // Format Price column
                if (dgvMyGames.Columns.Contains("Price")) dgvMyGames.Columns["Price"].DefaultCellStyle.Format = "C2"; // Currency format

                // Auto-size columns for better readability
                dgvMyGames.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                // You might want to display the approval status clearly, e.g., by formatting the IsApproved column
                // or adding a calculated column in the DataTable if you were converting to DataTable.
                // For now, the bool column 'IsApproved' is visible.

            }
            catch (Exception ex)
            {
                // Log the exception (in a real application)
                Console.WriteLine("Error loading vendor games: " + ex.Message);
                MessageBox.Show("Error loading your games: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Linked to the btnAddGame.Click event in the designer
        // Opens the form to add a new game
        private void btnAddGame_Click(object sender, EventArgs e)
        {
            // Open the Add/Edit Game form in Add mode.
            // Pass the current vendor user and null for the game object.
            AddEditGameForm addGameForm = new AddEditGameForm(_currentVendorUser, null);
            // ShowDialog makes the form modal, blocking interaction with the parent form
            addGameForm.ShowDialog();

            // Refresh the vendor's game list when the Add/Edit form is closed,
            // assuming a game might have been added.
            LoadVendorGames();
        }

        // Linked to the btnEditGame.Click event in the designer
        // Opens the form to edit the selected game
        private void btnEditGame_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dgvMyGames.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvMyGames.SelectedRows[0];
                // Get the GameID from the selected row (assuming it's a hidden column or property)
                int gameId = (int)selectedRow.Cells["GameID"].Value;

                try
                {
                    // Retrieve the full game details from the database using the BLL
                    Game gameToEdit = _gameBLL.GetGameById(gameId); // Assuming GetGameById is implemented in GameBLL/DAL

                    if (gameToEdit != null)
                    {
                        // Open the Add/Edit Game form in Edit mode, passing the game object
                        AddEditGameForm editGameForm = new AddEditGameForm(_currentVendorUser, gameToEdit);
                        editGameForm.ShowDialog();

                        // Refresh the vendor's game list when the Add/Edit form is closed
                        LoadVendorGames();
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
                    MessageBox.Show("Error retrieving game details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a game to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Linked to the btnDeleteGame.Click event in the designer
        // Deletes the selected game
        private void btnDeleteGame_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dgvMyGames.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvMyGames.SelectedRows[0];
                int gameId = (int)selectedRow.Cells["GameID"].Value;
                string gameTitle = selectedRow.Cells["Title"].Value.ToString();

                // Confirm the deletion action with the user
                DialogResult confirm = MessageBox.Show($"Are you sure you want to delete the game '{gameTitle}'?\n" +
                                                     "WARNING: This may fail if there are active rentals.",
                                                     "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Call the BLL method to delete the game.
                        // The BLL method should contain the business logic check for active rentals
                        // and potentially verify that the current vendor owns the game.
                        bool success = _gameBLL.DeleteGame(gameId); // Assuming DeleteGame is implemented in GameBLL/DAL

                        if (success)
                        {
                            MessageBox.Show($"Game '{gameTitle}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadVendorGames(); // Refresh the vendor's game list
                        }
                        else
                        {
                            // The BLL/DAL method returned false, likely due to active rentals or permission issues
                            MessageBox.Show($"Failed to delete game '{gameTitle}'.\nReason: There are active rentals for this game or you don't own it.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void VendorMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm(); // Create a new instance of the Login form
            loginForm.Show();
        }

        // Optional: Add a refresh button for the games list (linked in designer)
        private void btnRefreshGames_Click(object sender, EventArgs e)
        {
            LoadVendorGames(); // Reload the vendor's games
        }

        // Placeholder VendorBLL (Should be in BLL/VendorBLL.cs)
        public class VendorBLL
        {
            // Needs VendorDAL implementation
            private readonly DAL.VendorDAL _dal = new DAL.VendorDAL();

            // Needs DAL method SELECT VendorID FROM Vendors WHERE UserID = @UserID
            public int GetVendorIdByUserId(int userId)
            {
                // Call the DAL method
                return _dal.GetVendorIdByUserId(userId); // VendorDAL.GetVendorIdByUserId() needs implementation
            }

            // Need a method to get all vendors (VendorID, CompanyName) - used in AddEditGameForm for Admin
            public List<Vendor> GetAllVendors()
            {
                // Call the DAL method
                return _dal.GetAllVendors(); // VendorDAL.GetAllVendors() needs implementation
            }
        }

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
using GameRentalSystem.BLL;
using GameRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data; // Needed for DataTable
using System.Windows.Forms;

namespace GameRentalSystem.UI
{
    public partial class ClientMainForm : Form
    {
        private readonly User _currentUser;
        private readonly GameBLL _gameBLL = new GameBLL();
        private readonly RentalBLL _rentalBLL = new RentalBLL();
        private readonly AdminBLL _adminBLL = new AdminBLL(); // For admin request functionality

        public ClientMainForm(User user)
        {
            InitializeComponent(); // This calls the code in ClientMainForm.Designer.cs
            _currentUser = user;
            lblWelcome.Text = $"Welcome, {_currentUser.FullName} ({_currentUser.Role})!";

            // Load initial data when the form opens
            LoadApprovedGames();
            LoadClientRentals();
        }

        private void LoadApprovedGames()
        {
            try
            {
                // Get the list of approved games from the BLL
                List<Game> games = _gameBLL.GetApprovedGamesForClient();

                // Convert List<Game> to DataTable for easy DataGridView binding
                // This is a common approach when binding complex objects or needing specific column control
                DataTable dt = new DataTable();
                dt.Columns.Add("GameID", typeof(int));
                dt.Columns.Add("Title");
                dt.Columns.Add("Vendor"); // Assuming Game model has VendorName populated by join
                dt.Columns.Add("Category"); // Assuming Game model has CategoryName populated by join
                dt.Columns.Add("ReleaseYear", typeof(int));
                dt.Columns.Add("Price", typeof(decimal));

                foreach (var game in games)
                {
                    dt.Rows.Add(game.GameID, game.Title, game.VendorName, game.CategoryName, game.ReleaseYear, game.Price);
                }

                dgvApprovedGames.DataSource = dt;

                // Optional: Customize DataGridView appearance
                if (dgvApprovedGames.Columns.Contains("GameID")) dgvApprovedGames.Columns["GameID"].Visible = false; // Hide the ID column
                if (dgvApprovedGames.Columns.Contains("Price")) dgvApprovedGames.Columns["Price"].DefaultCellStyle.Format = "C2"; // Format price as currency
                                                                                                                                  // Auto-size columns for better readability
                dgvApprovedGames.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);


            }
            catch (Exception ex)
            {
                // Log the exception (in a real application)
                Console.WriteLine("Error loading approved games: " + ex.Message);
                MessageBox.Show("Error loading available games: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClientRentals()
        {
            try
            {
                // Get the list of rentals for the current client from the BLL
                List<Rental> rentals = _rentalBLL.GetClientRentals(_currentUser.UserID); // Assuming GetClientRentals is implemented in RentalBLL/DAL

                // Convert List<Rental> to DataTable for DataGridView binding
                DataTable dt = new DataTable();
                dt.Columns.Add("RentalID", typeof(int));
                dt.Columns.Add("GameTitle"); // Assuming Rental model has GameTitle populated by join
                dt.Columns.Add("RentalDate", typeof(DateTime));
                dt.Columns.Add("DueDate", typeof(DateTime));
                dt.Columns.Add("ReturnDate", typeof(DateTime)); // Nullable DateTime
                dt.Columns.Add("Status");

                foreach (var rental in rentals)
                {
                    // Use DBNull.Value for nullable DateTime properties that are null
                    dt.Rows.Add(rental.RentalID, rental.GameTitle, rental.RentalDate, rental.DueDate, rental.ReturnDate ?? (object)DBNull.Value, rental.Status);
                }

                dgvMyRentals.DataSource = dt;

                // Optional: Customize DataGridView appearance
                if (dgvMyRentals.Columns.Contains("RentalID")) dgvMyRentals.Columns["RentalID"].Visible = false; // Hide the ID column
                if (dgvMyRentals.Columns.Contains("RentalDate")) dgvMyRentals.Columns["RentalDate"].DefaultCellStyle.Format = "yyyy-MM-dd"; // Example date format
                if (dgvMyRentals.Columns.Contains("DueDate")) dgvMyRentals.Columns["DueDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                if (dgvMyRentals.Columns.Contains("ReturnDate")) dgvMyRentals.Columns["ReturnDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dgvMyRentals.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error loading client rentals: " + ex.Message);
                MessageBox.Show("Error loading your rentals: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Linked to btnRentGame.Click event in the designer
        private void btnRentGame_Click(object sender, EventArgs e)
        {
            // Get the selected game from the DataGridView
            if (dgvApprovedGames.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvApprovedGames.SelectedRows[0];
                // Get the GameID from the selected row
                int gameId = (int)selectedRow.Cells["GameID"].Value;
                string gameTitle = selectedRow.Cells["Title"].Value.ToString();

                // Confirm the rental action with the user
                DialogResult confirm = MessageBox.Show($"Are you sure you want to rent '{gameTitle}'?", "Confirm Rental", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Call the BLL method to perform the rental
                        bool success = _rentalBLL.RentGame(gameId, _currentUser.UserID); // Assuming RentGame is implemented in RentalBLL

                        if (success)
                        {
                            MessageBox.Show($"You have successfully rented '{gameTitle}'.", "Rental Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadClientRentals(); // Refresh client's rentals list to show the new rental
                        }
                        else
                        {
                            // The BLL method returned false, indicating a business logic constraint was violated
                            // (e.g., game not approved, or client already has an active rental)
                            MessageBox.Show($"Could not rent '{gameTitle}'. You might already have an active rental for this game or the game is unavailable.", "Rental Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine("Error during rental: " + ex.Message);
                        MessageBox.Show("Error during rental: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a game to rent.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Linked to btnReturnGame.Click event in the designer
        private void btnReturnGame_Click(object sender, EventArgs e)
        {
            // Get the selected rental from the DataGridView
            if (dgvMyRentals.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvMyRentals.SelectedRows[0];
                // Get the RentalID from the selected row
                int rentalId = (int)selectedRow.Cells["RentalID"].Value;
                string gameTitle = selectedRow.Cells["GameTitle"].Value.ToString();
                string status = selectedRow.Cells["Status"].Value.ToString();

                // Business Logic: Check if the rental is currently 'Rented' before allowing return
                if (status != "Rented")
                {
                    MessageBox.Show($"This rental is already '{status}'. It cannot be returned.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm the return action with the user
                DialogResult confirm = MessageBox.Show($"Are you sure you want to return '{gameTitle}'?", "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Call the BLL method to perform the return
                        // Pass client ID for verification within the BLL method (optional but good practice)
                        bool success = _rentalBLL.ReturnGame(rentalId, _currentUser.UserID); // Assuming ReturnGame is implemented in RentalBLL

                        if (success)
                        {
                            MessageBox.Show($"You have successfully returned '{gameTitle}'.", "Return Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadClientRentals(); // Refresh client's rentals list to show updated status and return date
                        }
                        else
                        {
                            // The BLL method returned false, indicating a business logic constraint was violated
                            // (e.g., rental not found, doesn't belong to client, or wasn't 'Rented')
                            MessageBox.Show($"Could not return '{gameTitle}'. Please check the rental status or contact support.", "Return Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine("Error during return: " + ex.Message);
                        MessageBox.Show("Error during return: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a rental to return.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Linked to btnRequestAdminRole.Click event in the designer
        private void btnRequestAdminRole_Click(object sender, EventArgs e)
        {
            // Confirm the request action with the user
            DialogResult confirm = MessageBox.Show("Are you sure you want to request to become an Admin?", "Confirm Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Call the BLL method to submit the admin request
                    bool success = _adminBLL.RequestAdminRole(_currentUser.UserID); // Assuming RequestAdminRole is implemented in AdminBLL

                    if (success)
                    {
                        MessageBox.Show("Your request to become an Admin has been submitted. Please wait for admin approval.", "Request Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Disable the button to prevent multiple requests from the same client
                        btnRequestAdminRole.Enabled = false;
                    }
                    else
                    {
                        // The BLL method returned false, indicating a business logic constraint was violated
                        // (e.g., client already has a pending request)
                        MessageBox.Show("Could not submit request. You might already have a pending request.", "Request Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine("Error submitting admin request: " + ex.Message);
                    MessageBox.Show("Error submitting request: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Handle form closing (linked to the FormClosing event in the designer)
        private void ClientMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoginForm loginForm = new LoginForm(); // Create a new instance of the Login form
            loginForm.Show();
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
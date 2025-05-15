using GameRentalSystem.BLL;
using GameRentalSystem.Models;
using GameRentalSystem.DAL; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient; // Needed for placeholder DAL methods
using System.Data; // Needed for DataTable if using ExecuteDataTable

namespace GameRentalSystem.UI
{
    public partial class AddEditGameForm : Form
    {
        private readonly User _currentUser; // The user adding/editing (Admin or Vendor)
        private Game _gameToEdit; // Null if adding, populated if editing

        private readonly GameBLL _gameBLL = new GameBLL();
        // In a real app, these would be separate BLL classes in the BLL folder
        private readonly CategoryBLL _categoryBLL = new CategoryBLL();
        private readonly VendorBLL _vendorBLL = new VendorBLL();


        // Constructor for Add/Edit form
        public AddEditGameForm(User currentUser, Game gameToEdit)
        {
            InitializeComponent(); // This calls the code in AddEditGameForm.Designer.cs
            _currentUser = currentUser;
            _gameToEdit = gameToEdit; // Will be null in Add mode

            PopulateCategories();

            // Vendors dropdown and IsApproved checkbox are only relevant for Admin
            if (_currentUser.Role == "Admin")
            {
                PopulateVendors();
                lblVendor.Visible = true;
                cmbVendor.Visible = true;
                lblIsApproved.Visible = true;
                chkIsApproved.Visible = true;
            }
            else // Vendor or other roles
            {
                lblVendor.Visible = false;
                cmbVendor.Visible = false;
                lblIsApproved.Visible = false;
                chkIsApproved.Visible = false;
            }

            // Configure form based on Add or Edit mode
            if (_gameToEdit == null)
            {
                // Add Mode
                this.Text = "Add New Game";
                btnSave.Text = "Add Game";
                // Default IsApproved to false for new games (vendors submit unapproved)
                // Admin can choose to approve immediately if they add it.
                chkIsApproved.Checked = false;
                // Ensure Vendor dropdown is enabled for Admin in Add mode
                if (_currentUser.Role == "Admin")
                {
                    cmbVendor.Enabled = true;
                }
            }
            else
            {
                // Edit Mode
                this.Text = "Edit Game";
                btnSave.Text = "Save Changes";
                LoadGameDetails(_gameToEdit); // Populate fields with existing data

                // Prevent non-Admin users (Vendors) from changing the Vendor of a game
                if (_currentUser.Role != "Admin")
                {
                    cmbVendor.Enabled = false;
                }
                // Prevent non-Admin users (Vendors) from changing approval status
                if (_currentUser.Role != "Admin")
                {
                    chkIsApproved.Enabled = false;
                }
            }
        }

        private void PopulateCategories()
        {
            try
            {
                List<Category> categories = _categoryBLL.GetAllCategories(); // Call the BLL method
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "Name"; // Display the category name
                cmbCategory.ValueMember = "CategoryID"; // Use CategoryID as the underlying value
                cmbCategory.SelectedIndex = -1; // No item selected initially
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error loading categories: " + ex.Message);
                MessageBox.Show("Error loading categories: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateVendors()
        {
            try
            {
                List<Vendor> vendors = _vendorBLL.GetAllVendors(); // Call the BLL method
                cmbVendor.DataSource = vendors;
                cmbVendor.DisplayMember = "CompanyName"; // Display company name
                cmbVendor.ValueMember = "VendorID"; // Use VendorID as the underlying value
                cmbVendor.SelectedIndex = -1; // No item selected initially
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error loading vendors: " + ex.Message);
                MessageBox.Show("Error loading vendors: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGameDetails(Game game)
        {
            txtTitle.Text = game.Title;
            txtDescription.Text = game.Description;
            txtReleaseYear.Text = game.ReleaseYear.ToString();
            txtPrice.Text = game.Price.ToString();

            // Select the correct category in the dropdown
            // Use FindByValue to select the item by its ValueMember (CategoryID)
            cmbCategory.SelectedValue = game.CategoryID;


            // Select the correct vendor in the dropdown (only visible/enabled for Admin)
            if (_currentUser.Role == "Admin")
            {
                cmbVendor.SelectedValue = game.VendorID;
                chkIsApproved.Checked = game.IsApproved;
            }
        }


        // Linked to btnSave.Click event in designer
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get data from controls
            string title = txtTitle.Text.Trim(); // Trim whitespace
            string description = txtDescription.Text.Trim();
            int releaseYear;
            decimal price;
            // Get selected CategoryID. Handle case where nothing is selected.
            int categoryId = -1;
            if (cmbCategory.SelectedValue != null)
            {
                categoryId = (int)cmbCategory.SelectedValue;
            }

            int vendorId = -1;
            bool isApproved = false;
            int? adminApproverId = null;
            DateTime? approvalDate = null;


            // Validation
            if (string.IsNullOrWhiteSpace(title) || !int.TryParse(txtReleaseYear.Text, out releaseYear) || releaseYear <= 0 ||
                !decimal.TryParse(txtPrice.Text, out price) || price < 0 || categoryId == -1)
            {
                MessageBox.Show("Please fill in all required fields correctly (Title, Release Year, Price, Category).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentUser.Role == "Admin")
            {
                // Get selected VendorID. Handle case where nothing is selected.
                if (cmbVendor.SelectedValue != null)
                {
                    vendorId = (int)cmbVendor.SelectedValue;
                }

                if (vendorId == -1)
                {
                    MessageBox.Show("Please select a vendor.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                isApproved = chkIsApproved.Checked;
                if (isApproved)
                {
                    adminApproverId = _currentUser.UserID;
                    approvalDate = DateTime.Now;
                }
            }
            else if (_currentUser.Role == "Vendor")
            {
                // Get the VendorID for the current logged-in vendor from the BLL
                vendorId = _vendorBLL.GetVendorIdByUserId(_currentUser.UserID); // Call the BLL method
                if (vendorId == -1)
                {
                    MessageBox.Show("Could not find vendor ID for the current user. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                isApproved = false; // Vendors' games are not approved on submission
                adminApproverId = null; // No admin approver yet
                approvalDate = null; // No approval date yet
            }
            else
            {
                // Should not happen based on login logic, but as a safeguard
                MessageBox.Show("You do not have permission to add or edit games.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            try
            {
                bool success;
                Game gameToSave = new Game
                {
                    Title = title,
                    Description = description,
                    ReleaseYear = releaseYear,
                    Price = price,
                    VendorID = vendorId,
                    CategoryID = categoryId,
                    IsApproved = isApproved,
                    AdminApproverID = adminApproverId,
                    ApprovalDate = approvalDate
                };


                if (_gameToEdit == null)
                {
                    // Add Mode
                    success = _gameBLL.AddGame(gameToSave, _currentUser.UserID, _currentUser.Role);

                    if (success)
                    {
                        MessageBox.Show("Game added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK; // Indicate success to the calling form
                        this.Close(); // Close form after adding
                    }
                    else
                    {
                        MessageBox.Show("Failed to add game. Please check input or try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Edit Mode
                    gameToSave.GameID = _gameToEdit.GameID; // Keep the original GameID

                    success = _gameBLL.UpdateGame(gameToSave, _currentUser.UserID, _currentUser.Role);

                    if (success)
                    {
                        MessageBox.Show("Game updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK; // Indicate success to the calling form
                        this.Close(); // Close form after editing
                    }
                    else
                    {
                        MessageBox.Show("Failed to update game.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("An error occurred while saving the game: " + ex.Message);
                MessageBox.Show("An unexpected error occurred while saving the game: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Linked to btnCancel.Click event in designer
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Indicate cancellation
            this.Close(); // Close the form without saving
        }

      
        public class CategoryBLL
        {
            private readonly CategoryDAL _dal = new CategoryDAL(); // Needs CategoryDAL implementation
            public List<Category> GetAllCategories()
            {
                // Call the DAL method
                return _dal.GetAllCategories(); // CategoryDAL.GetAllCategories() needs implementation
            }
        }

        // Placeholder VendorBLL (Should be in BLL/VendorBLL.cs)
        public class VendorBLL
        {
            private readonly VendorDAL _dal = new VendorDAL(); // Needs VendorDAL implementation
                                                               // Needs DAL method SELECT VendorID, CompanyName FROM Vendors JOIN Users ON Vendors.UserID = Users.UserID
            public List<Vendor> GetAllVendors()
            {
                // Call the DAL method
                return _dal.GetAllVendors(); // VendorDAL.GetAllVendors() needs implementation
            }
            public int GetVendorIdByUserId(int userId)
            {
                // Call the DAL method
                return _dal.GetVendorIdByUserId(userId); // VendorDAL.GetVendorIdByUserId() needs implementation
            }
        }

        // Placeholder CategoryDAL (Should be in DAL/CategoryDAL.cs)
        public class CategoryDAL
        {
            // Assuming DatabaseHelper is in DAL
            public List<Category> GetAllCategories()
            {
                List<Category> categories = new List<Category>();
                string commandText = "SELECT CategoryID, Name, Description FROM Categories ORDER BY Name";

                // Assuming DatabaseHelper.ExecuteReader is implemented
                using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText))
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"))
                        });
                    }
                }
                return categories;
            }
        }

        // Placeholder VendorDAL (Should be in DAL/VendorDAL.cs)
        public class VendorDAL
        {
            // Assuming DatabaseHelper is in DAL
            public int GetVendorIdByUserId(int userId)
            {
                string commandText = "SELECT VendorID FROM Vendors WHERE UserID = @UserID";
                // Assuming DatabaseHelper.ExecuteScalar is implemented
                SqlParameter[] parameters = new SqlParameter[]
                {
                      new SqlParameter("@UserID", userId)
                };
                object result = DatabaseHelper.ExecuteScalar(commandText, parameters);
                return result == null ? -1 : (int)result;
            }

            // Need a method to get all vendors (VendorID, CompanyName)
            public List<Vendor> GetAllVendors()
            {
                List<Vendor> vendors = new List<Vendor>();
                // Join with Users to get UserID if needed, but CompanyName is in Vendors table
                string commandText = "SELECT VendorID, CompanyName, UserID FROM Vendors ORDER BY CompanyName";

                // Assuming DatabaseHelper.ExecuteReader is implemented
                using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText))
                {
                    while (reader.Read())
                    {
                        vendors.Add(new Vendor
                        {
                            VendorID = reader.GetInt32(reader.GetOrdinal("VendorID")),
                            CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID"))
                            // ContactNumber is not needed for the dropdown, but could be included
                        });
                    }
                }
                return vendors;
            }

          
        }

      
    }
}
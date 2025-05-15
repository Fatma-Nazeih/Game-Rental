using GameRentalSystem.BLL;
using GameRentalSystem.Models;
using System;
using System.Windows.Forms;

namespace GameRentalSystem.UI
{
    public partial class RegisterForm : Form
    {
        private readonly AuthBLL _authBLL = new AuthBLL();

        public RegisterForm()
        {
            InitializeComponent(); // This calls the code in RegisterForm.Designer.cs
            PopulateRoles(); // Populate the role dropdown on form load
            SetupRoleSelection(); // Set up event handling for role selection
        }

        // Populates the cmbRole dropdown with available registration roles
        private void PopulateRoles()
        {
            // Allow registration for Client and Vendor roles
            cmbRole.Items.Add("Client");
            cmbRole.Items.Add("Vendor");
            cmbRole.SelectedIndex = 0; // Default selection to "Client"
        }

        // Sets up the event handler for the cmbRole selection change
        private void SetupRoleSelection()
        {
            // Hook up the event to the handler method
            cmbRole.SelectedIndexChanged += cmbRole_SelectedIndexChanged;
            // Trigger the handler once initially to set visibility based on the default selection
            cmbRole_SelectedIndexChanged(this, EventArgs.Empty);
        }

        // Handles the event when the selected role in cmbRole changes
        // Shows/hides vendor-specific fields based on the selected role
        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the selected item is "Vendor" (case-insensitive comparison is safer)
            bool isVendor = cmbRole.SelectedItem?.ToString().Equals("Vendor", StringComparison.OrdinalIgnoreCase) ?? false;

            // Show or hide the labels and text boxes for Company Name and Contact Number
            lblCompanyName.Visible = isVendor;
            txtCompanyName.Visible = isVendor;
            lblContactNumber.Visible = isVendor;
            txtContactNumber.Visible = isVendor;

            // Clear the text from vendor fields if they are being hidden
            if (!isVendor)
            {
                txtCompanyName.Clear();
                txtContactNumber.Clear();
            }
        }


        // Linked to the btnRegister.Click event in the designer
        // Handles the user registration process
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Retrieve user input, trimming whitespace from text fields
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text; // WARNING: Plain text password (should be hashed in real app)
            string confirmPassword = txtConfirmPassword.Text;
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString(); // Get the selected role string
            string companyName = txtCompanyName.Text.Trim(); // Vendor specific
            string contactNumber = txtContactNumber.Text.Trim(); // Vendor specific

            // Basic input validation: Check for empty or whitespace fields
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) || string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop the registration process
            }

            // Validate that password and confirm password match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear(); // Clear password fields for security
                txtConfirmPassword.Clear();
                txtPassword.Focus(); // Set focus back to password field
                return; // Stop the registration process
            }

            // Validate vendor-specific fields if the selected role is Vendor
            if (role.Equals("Vendor", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(companyName) /*|| string.IsNullOrWhiteSpace(contactNumber)*/) // Decide if contact number is mandatory based on requirements
                {
                    MessageBox.Show("Please provide Company Name for Vendor registration.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop the registration process
                }
            }

            // Create a User object with the collected data
            User newUser = new User
            {
                Username = username,
                Password = password, // WARNING: Plain text password
                FullName = fullName,
                Email = email,
                Role = role
            };

            try
            {
                bool success;
                if (role.Equals("Client", StringComparison.OrdinalIgnoreCase))
                {
                    // Call the BLL to register a client
                    success = _authBLL.RegisterClient(newUser);
                }
                else // Assume role is "Vendor"
                {
                    // Create a Vendor object with vendor-specific details
                    Vendor vendorDetails = new Vendor
                    {
                        CompanyName = companyName,
                        ContactNumber = contactNumber // Can be null if not mandatory
                    };
                    // Call the BLL to register a vendor (which includes creating the User and Vendor entries)
                    success = _authBLL.RegisterVendor(newUser, vendorDetails);
                }


                if (success)
                {
                    // Registration was successful
                    MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Close the registration form
                }
                else
                {
                    // Registration failed (e.g., username or email already exists - handled by BLL)
                    MessageBox.Show("Registration failed. Username or email might already exist.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (in a real application)
                Console.WriteLine("An error occurred during registration: " + ex.Message);
                MessageBox.Show("An error occurred during registration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Linked to the btnCancel.Click event in the designer
        // Closes the registration form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the registration form
        }

        

        // Optional: Add more advanced validation like email format checking
        // private bool IsValidEmail(string email) { ... }
    }
}
using GameRentalSystem.BLL;
using GameRentalSystem.Models;
using System;
using System.Windows.Forms;

namespace GameRentalSystem.UI
{
    public partial class LoginForm : Form
    {
        private readonly AuthBLL _authBLL = new AuthBLL();

        public LoginForm()
        {
            InitializeComponent(); // This calls the code in LoginForm.Designer.cs
            // Optional: Add event handlers for Enter key press
            txtUsername.KeyPress += TxtUsername_KeyPress;
            txtPassword.KeyPress += TxtPassword_KeyPress;
        }

        // This method is linked to the btnLogin.Click event in the designer
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim(); // Trim whitespace
            string password = txtPassword.Text; // WARNING: Plain text password

            // Basic input validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Call the BLL to authenticate the user
                User authenticatedUser = _authBLL.AuthenticateUser(username, password);

                if (authenticatedUser != null)
                {
                    // Successful login
                    MessageBox.Show($"Welcome, {authenticatedUser.FullName} ({authenticatedUser.Role})!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Open the appropriate main form based on role
                    this.Hide(); // Hide the login form

                    Form mainForm = null;
                    switch (authenticatedUser.Role)
                    {
                        case "Admin":
                            mainForm = new AdminMainForm(authenticatedUser);
                            break;
                        case "Client":
                            mainForm = new ClientMainForm(authenticatedUser);
                            break;
                        case "Vendor":
                            mainForm = new VendorMainForm(authenticatedUser);
                            break;
                        default:
                            // Handle unexpected roles
                            MessageBox.Show("Unknown user role. Please contact support.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Show(); // Show login form again
                            return; // Exit the event handler
                    }

                    // Show the main form
                    mainForm.Show();

                }
                else
                {
                    // Failed login (invalid credentials)
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear(); // Clear password field for security
                    txtUsername.Focus(); // Set focus back to username field
                }
            }
            catch (Exception ex)
            {
                // Log the exception (in a real application)
                Console.WriteLine("An error occurred during login: " + ex.Message);
                MessageBox.Show("An error occurred during login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // This method is linked to the btnRegister.Click event in the designer
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Open the Registration form as a modal dialog
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
            // After closing the dialog, the login form remains visible
        }

        // Handle form closing to ensure the entire application exits
        // This method is linked to the FormClosing event in the designer
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Ensure the application exits when the login form is closed
        }

        // Optional: Handle Enter key press in username field to move to password or trigger login
        private void TxtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    // If password field is not empty, trigger login
                    btnLogin_Click(sender, e);
                }
                else
                {
                    // Otherwise, move focus to the password field
                    txtPassword.Focus();
                }
                e.Handled = true; // Prevent the default Enter key behavior (like a 'ding' sound)
            }
        }

        // Optional: Handle Enter key press in password field to trigger login
        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Trigger the login button click event
                btnLogin_Click(sender, e);
                e.Handled = true; // Prevent default Enter key behavior
            }
        }

        
    }
}
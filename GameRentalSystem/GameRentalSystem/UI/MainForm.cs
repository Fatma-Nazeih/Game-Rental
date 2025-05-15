using GameRentalSystem.Models;
using System;
using System.Windows.Forms;
using GameRentalSystem.UI; // Make sure this namespace is correct

namespace GameRentalSystem.UI
{
    // This form acts as an intermediary after login to direct the user
    // to the appropriate role-specific main form.
    // In many architectures, the LoginForm handles this redirection directly.
    public partial class MainForm : Form
    {
        private readonly User _authenticatedUser;

        // Constructor receives the authenticated user from the LoginForm
        public MainForm(User authenticatedUser)
        {
            InitializeComponent(); // This calls the code in MainForm.Designer.cs
            _authenticatedUser = authenticatedUser;

            // This form's purpose is to immediately redirect, so it doesn't need
            // to be visible for long. We can hide it right away or make it
            // invisible in the designer properties.
            this.Opacity = 0; // Make it invisible initially
            this.ShowInTaskbar = false; // Don't show in taskbar
        }

        // Use the Load event to perform the redirection logic
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Decide which main form to open based on the user's role
            Form roleSpecificMainForm = null;

            switch (_authenticatedUser.Role)
            {
                case "Admin":
                    roleSpecificMainForm = new AdminMainForm(_authenticatedUser);
                    break;
                case "Client":
                    roleSpecificMainForm = new ClientMainForm(_authenticatedUser);
                    break;
                case "Vendor":
                    roleSpecificMainForm = new VendorMainForm(_authenticatedUser);
                    break;
                default:
                    // Handle unexpected roles - maybe show an error and return to login
                    MessageBox.Show("Unknown user role. Cannot open main form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Close this form and potentially show the login form again
                    this.Close();
                    // You might need to handle showing the login form in Program.cs or similar
                    return; // Exit the Load event
            }

            // Show the determined role-specific main form
            if (roleSpecificMainForm != null)
            {
                roleSpecificMainForm.Show();
                // Close this intermediary form immediately after showing the next one
                this.Close();
            }
            else
            {
                // If no form was determined (should be caught by default case), close this form
                this.Close();
            }
        }

        // This form doesn't really need other event handlers if its only job is redirection
        // However, if you wanted it to be a base form, you'd add common logic here.

        // Handle form closing to ensure the application exits correctly
        // This is important if this is the *first* form shown after login.
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If this form is closed, ensure the whole application exits.
            // This might need adjustment depending on your application's startup logic.
            // If LoginForm is the startup form, its FormClosing handles exit.
            // If MainForm is the startup form, this should call Application.Exit().
            // Assuming LoginForm is the startup form and handles Application.Exit(),
            // this MainForm just needs to close itself. If MainForm is the startup,
            // uncomment the line below:
            // Application.Exit();
        }
    }
}
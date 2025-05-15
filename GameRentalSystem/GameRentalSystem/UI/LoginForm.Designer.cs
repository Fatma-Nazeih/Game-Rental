namespace GameRentalSystem.UI
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnRegister = new Button();
            SuspendLayout();

            // Form Styling
            BackColor = Color.FromArgb(30, 30, 40); // Dark background
            ForeColor = Color.White;
            Font = new Font("Outfit", 10F, FontStyle.Regular);
            Text = "Game Rental - Login";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Padding = new Padding(20);

            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(40, 40);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(70, 17);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username:";

            // txtUsername
            txtUsername.BackColor = Color.FromArgb(50, 50, 60);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.ForeColor = Color.White;
            txtUsername.Location = new Point(120, 37);
            txtUsername.Size = new Size(200, 25);
            txtUsername.TabIndex = 1;

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(40, 80);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(67, 17);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password:";

            // txtPassword
            txtPassword.BackColor = Color.FromArgb(50, 50, 60);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.ForeColor = Color.White;
            txtPassword.Location = new Point(120, 77);
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(200, 25);
            txtPassword.TabIndex = 3;

            // btnLogin
            btnLogin.BackColor = Color.FromArgb(0, 150, 136); // Teal
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(120, 120);
            btnLogin.Size = new Size(90, 32);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;

            // btnRegister
            btnRegister.BackColor = Color.FromArgb(69, 90, 100); // Dark teal
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(230, 120);
            btnRegister.Size = new Size(90, 32);
            btnRegister.TabIndex = 5;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;

            // LoginForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 200);
            Controls.Add(btnRegister);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Name = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
    }
}
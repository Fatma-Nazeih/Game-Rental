namespace GameRentalSystem.UI
{
    partial class RegisterForm
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
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblRole = new Label();
            cmbRole = new ComboBox();
            lblCompanyName = new Label();
            txtCompanyName = new TextBox();
            lblContactNumber = new Label();
            txtContactNumber = new TextBox();
            btnRegister = new Button();
            btnCancel = new Button();
            SuspendLayout();

            // Form Styling
            BackColor = Color.FromArgb(30, 30, 40); // Dark background
            ForeColor = Color.White;
            Font = new Font("Outfit", 10F, FontStyle.Regular);
            Text = "Game Rental - Register";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Padding = new Padding(20);
            ClientSize = new Size(450, 420);

            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(30, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(70, 17);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username:";

            // txtUsername
            txtUsername.BackColor = Color.FromArgb(50, 50, 60);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.ForeColor = Color.White;
            txtUsername.Location = new Point(160, 27);
            txtUsername.Size = new Size(240, 25);
            txtUsername.TabIndex = 1;

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(30, 70);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(67, 17);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password:";

            // txtPassword
            txtPassword.BackColor = Color.FromArgb(50, 50, 60);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.ForeColor = Color.White;
            txtPassword.Location = new Point(160, 67);
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(240, 25);
            txtPassword.TabIndex = 3;

            // lblConfirmPassword
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new Point(30, 110);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(120, 17);
            lblConfirmPassword.TabIndex = 4;
            lblConfirmPassword.Text = "Confirm Password:";

            // txtConfirmPassword
            txtConfirmPassword.BackColor = Color.FromArgb(50, 50, 60);
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.ForeColor = Color.White;
            txtConfirmPassword.Location = new Point(160, 107);
            txtConfirmPassword.PasswordChar = '•';
            txtConfirmPassword.Size = new Size(240, 25);
            txtConfirmPassword.TabIndex = 5;

            // lblFullName
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(30, 150);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(71, 17);
            lblFullName.TabIndex = 6;
            lblFullName.Text = "Full Name:";

            // txtFullName
            txtFullName.BackColor = Color.FromArgb(50, 50, 60);
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            txtFullName.ForeColor = Color.White;
            txtFullName.Location = new Point(160, 147);
            txtFullName.Size = new Size(240, 25);
            txtFullName.TabIndex = 7;

            // lblEmail
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(30, 190);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(42, 17);
            lblEmail.TabIndex = 8;
            lblEmail.Text = "Email:";

            // txtEmail
            txtEmail.BackColor = Color.FromArgb(50, 50, 60);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.ForeColor = Color.White;
            txtEmail.Location = new Point(160, 187);
            txtEmail.Size = new Size(240, 25);
            txtEmail.TabIndex = 9;

            // lblRole
            lblRole.AutoSize = true;
            lblRole.Location = new Point(30, 230);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(36, 17);
            lblRole.TabIndex = 10;
            lblRole.Text = "Role:";

            // cmbRole
            cmbRole.BackColor = Color.FromArgb(50, 50, 60);
            cmbRole.FlatStyle = FlatStyle.Flat;
            cmbRole.ForeColor = Color.White;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(160, 227);
            cmbRole.Size = new Size(160, 25);
            cmbRole.TabIndex = 11;

            // lblCompanyName
            lblCompanyName.AutoSize = true;
            lblCompanyName.Location = new Point(30, 270);
            lblCompanyName.Name = "lblCompanyName";
            lblCompanyName.Size = new Size(109, 17);
            lblCompanyName.TabIndex = 12;
            lblCompanyName.Text = "Company Name:";
            lblCompanyName.Visible = false;

            // txtCompanyName
            txtCompanyName.BackColor = Color.FromArgb(50, 50, 60);
            txtCompanyName.BorderStyle = BorderStyle.FixedSingle;
            txtCompanyName.ForeColor = Color.White;
            txtCompanyName.Location = new Point(160, 267);
            txtCompanyName.Size = new Size(240, 25);
            txtCompanyName.TabIndex = 13;
            txtCompanyName.Visible = false;

            // lblContactNumber
            lblContactNumber.AutoSize = true;
            lblContactNumber.Location = new Point(30, 310);
            lblContactNumber.Name = "lblContactNumber";
            lblContactNumber.Size = new Size(111, 17);
            lblContactNumber.TabIndex = 14;
            lblContactNumber.Text = "Contact Number:";
            lblContactNumber.Visible = false;

            // txtContactNumber
            txtContactNumber.BackColor = Color.FromArgb(50, 50, 60);
            txtContactNumber.BorderStyle = BorderStyle.FixedSingle;
            txtContactNumber.ForeColor = Color.White;
            txtContactNumber.Location = new Point(160, 307);
            txtContactNumber.Size = new Size(240, 25);
            txtContactNumber.TabIndex = 15;
            txtContactNumber.Visible = false;

            // btnRegister
            btnRegister.BackColor = Color.FromArgb(0, 150, 136); // Teal
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(160, 350);
            btnRegister.Size = new Size(100, 32);
            btnRegister.TabIndex = 16;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;

            // btnCancel
            btnCancel.BackColor = Color.FromArgb(69, 90, 100); // Dark teal
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(270, 350);
            btnCancel.Size = new Size(100, 32);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;

            // RegisterForm
            Controls.Add(btnCancel);
            Controls.Add(btnRegister);
            Controls.Add(txtContactNumber);
            Controls.Add(lblContactNumber);
            Controls.Add(txtCompanyName);
            Controls.Add(lblCompanyName);
            Controls.Add(cmbRole);
            Controls.Add(lblRole);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtFullName);
            Controls.Add(lblFullName);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Name = "RegisterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblRole;
        private ComboBox cmbRole;
        private Label lblCompanyName;
        private TextBox txtCompanyName;
        private Label lblContactNumber;
        private TextBox txtContactNumber;
        private Button btnRegister;
        private Button btnCancel;
    }
}
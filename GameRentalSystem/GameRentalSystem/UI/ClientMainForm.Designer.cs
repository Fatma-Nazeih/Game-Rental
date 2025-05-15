namespace GameRentalSystem.UI
{
    partial class ClientMainForm
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
            Font = new Font("Outfit", 10F, FontStyle.Regular);
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblWelcome = new Label();
            lblAvailableGames = new Label();
            dgvApprovedGames = new DataGridView();
            btnRentGame = new Button();
            lblMyRentals = new Label();
            dgvMyRentals = new DataGridView();
            btnReturnGame = new Button();
            btnRequestAdminRole = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvApprovedGames).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMyRentals).BeginInit();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(25, 25);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(174, 25);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome, [Client]!";
            // 
            // lblAvailableGames
            // 
            lblAvailableGames.AutoSize = true;
            lblAvailableGames.Location = new Point(25, 70);
            lblAvailableGames.Name = "lblAvailableGames";
            lblAvailableGames.Size = new Size(129, 20);
            lblAvailableGames.TabIndex = 1;
            lblAvailableGames.Text = "Available Games:";
            // 
            // dgvApprovedGames
            // 
            dgvApprovedGames.AllowUserToAddRows = false;
            dgvApprovedGames.AllowUserToDeleteRows = false;
            dgvApprovedGames.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvApprovedGames.BackgroundColor = Color.FromArgb(40, 40, 50);
            dgvApprovedGames.BorderStyle = BorderStyle.None;
            dgvApprovedGames.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvApprovedGames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvApprovedGames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvApprovedGames.GridColor = Color.FromArgb(60, 60, 70);
            dgvApprovedGames.Location = new Point(25, 95);
            dgvApprovedGames.MultiSelect = false;
            dgvApprovedGames.Name = "dgvApprovedGames";
            dgvApprovedGames.ReadOnly = true;
            dgvApprovedGames.RowHeadersVisible = false;
            dgvApprovedGames.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 60);
            dgvApprovedGames.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
            dgvApprovedGames.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 107);
            dgvApprovedGames.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvApprovedGames.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvApprovedGames.Size = new Size(910, 180);
            dgvApprovedGames.TabIndex = 2;
            // 
            // btnRentGame
            // 
            btnRentGame.BackColor = Color.FromArgb(0, 150, 136);
            btnRentGame.FlatAppearance.BorderSize = 0;
            btnRentGame.FlatStyle = FlatStyle.Flat;
            btnRentGame.ForeColor = Color.White;
            btnRentGame.Location = new Point(25, 285);
            btnRentGame.Name = "btnRentGame";
            btnRentGame.Size = new Size(150, 35);
            btnRentGame.TabIndex = 3;
            btnRentGame.Text = "Rent Selected Game";
            btnRentGame.UseVisualStyleBackColor = false;
            btnRentGame.Click += btnRentGame_Click;
            // 
            // lblMyRentals
            // 
            lblMyRentals.AutoSize = true;
            lblMyRentals.Location = new Point(25, 340);
            lblMyRentals.Name = "lblMyRentals";
            lblMyRentals.Size = new Size(91, 20);
            lblMyRentals.TabIndex = 4;
            lblMyRentals.Text = "My Rentals:";
            // 
            // dgvMyRentals
            // 
            dgvMyRentals.AllowUserToAddRows = false;
            dgvMyRentals.AllowUserToDeleteRows = false;
            dgvMyRentals.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMyRentals.BackgroundColor = Color.FromArgb(40, 40, 50);
            dgvMyRentals.BorderStyle = BorderStyle.None;
            dgvMyRentals.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvMyRentals.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvMyRentals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyRentals.GridColor = Color.FromArgb(60, 60, 70);
            dgvMyRentals.Location = new Point(25, 365);
            dgvMyRentals.MultiSelect = false;
            dgvMyRentals.Name = "dgvMyRentals";
            dgvMyRentals.ReadOnly = true;
            dgvMyRentals.RowHeadersVisible = false;
            dgvMyRentals.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 60);
            dgvMyRentals.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
            dgvMyRentals.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 107);
            dgvMyRentals.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvMyRentals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMyRentals.Size = new Size(910, 150);
            dgvMyRentals.TabIndex = 5;
            // 
            // btnReturnGame
            // 
            btnReturnGame.BackColor = Color.FromArgb(69, 90, 100);
            btnReturnGame.FlatAppearance.BorderSize = 0;
            btnReturnGame.FlatStyle = FlatStyle.Flat;
            btnReturnGame.ForeColor = Color.White;
            btnReturnGame.Location = new Point(25, 525);
            btnReturnGame.Name = "btnReturnGame";
            btnReturnGame.Size = new Size(150, 35);
            btnReturnGame.TabIndex = 6;
            btnReturnGame.Text = "Return Selected";
            btnReturnGame.UseVisualStyleBackColor = false;
            btnReturnGame.Click += btnReturnGame_Click;
            // 
            // btnRequestAdminRole
            // 
            btnRequestAdminRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRequestAdminRole.BackColor = Color.FromArgb(120, 144, 156);
            btnRequestAdminRole.FlatAppearance.BorderSize = 0;
            btnRequestAdminRole.FlatStyle = FlatStyle.Flat;
            btnRequestAdminRole.ForeColor = Color.White;
            btnRequestAdminRole.Location = new Point(785, 25);
            btnRequestAdminRole.Name = "btnRequestAdminRole";
            btnRequestAdminRole.Size = new Size(150, 35);
            btnRequestAdminRole.TabIndex = 7;
            btnRequestAdminRole.Text = "Request Admin Role";
            btnRequestAdminRole.UseVisualStyleBackColor = false;
            btnRequestAdminRole.Click += btnRequestAdminRole_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(120, 50, 50);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(814, 535);
            button1.Name = "button1";
            button1.Size = new Size(100, 25);
            button1.TabIndex = 8;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // ClientMainForm
            // 
            BackColor = Color.FromArgb(30, 30, 40);
            ClientSize = new Size(960, 580);
            Controls.Add(button1);
            Controls.Add(btnRequestAdminRole);
            Controls.Add(btnReturnGame);
            Controls.Add(dgvMyRentals);
            Controls.Add(lblMyRentals);
            Controls.Add(btnRentGame);
            Controls.Add(dgvApprovedGames);
            Controls.Add(lblAvailableGames);
            Controls.Add(lblWelcome);

            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ClientMainForm";
            Padding = new Padding(20);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Game Rental System - Client Dashboard";
            ((System.ComponentModel.ISupportInitialize)dgvApprovedGames).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMyRentals).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblWelcome;
        private Label lblAvailableGames;
        private DataGridView dgvApprovedGames;
        private Button btnRentGame;
        private Label lblMyRentals;
        private DataGridView dgvMyRentals;
        private Button btnReturnGame;
        private Button btnRequestAdminRole;
        private Button button1;
    }
}
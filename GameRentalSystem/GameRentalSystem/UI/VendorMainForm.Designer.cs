namespace GameRentalSystem.UI
{
    partial class VendorMainForm
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
            lblWelcome = new Label();
            lblMyGames = new Label();
            dgvMyGames = new DataGridView();
            btnAddGame = new Button();
            btnEditGame = new Button();
            btnDeleteGame = new Button();
            btnRefreshGames = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMyGames).BeginInit();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(25, 25);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(189, 25);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome, [Vendor]!";
            // 
            // lblMyGames
            // 
            lblMyGames.AutoSize = true;
            lblMyGames.Location = new Point(25, 70);
            lblMyGames.Name = "lblMyGames";
            lblMyGames.Size = new Size(87, 20);
            lblMyGames.TabIndex = 1;
            lblMyGames.Text = "My Games:";
            // 
            // dgvMyGames
            // 
            dgvMyGames.AllowUserToAddRows = false;
            dgvMyGames.AllowUserToDeleteRows = false;
            dgvMyGames.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMyGames.BackgroundColor = Color.FromArgb(40, 40, 50);
            dgvMyGames.BorderStyle = BorderStyle.None;
            dgvMyGames.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvMyGames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvMyGames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyGames.GridColor = Color.FromArgb(60, 60, 70);
            dgvMyGames.Location = new Point(25, 95);
            dgvMyGames.MultiSelect = false;
            dgvMyGames.Name = "dgvMyGames";
            dgvMyGames.ReadOnly = true;
            dgvMyGames.RowHeadersVisible = false;
            dgvMyGames.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 60);
            dgvMyGames.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
            dgvMyGames.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 107);
            dgvMyGames.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvMyGames.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMyGames.Size = new Size(910, 350);
            dgvMyGames.TabIndex = 2;
            // 
            // btnAddGame
            // 
            btnAddGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddGame.BackColor = Color.FromArgb(0, 150, 136);
            btnAddGame.FlatAppearance.BorderSize = 0;
            btnAddGame.FlatStyle = FlatStyle.Flat;
            btnAddGame.ForeColor = Color.White;
            btnAddGame.Location = new Point(25, 460);
            btnAddGame.Name = "btnAddGame";
            btnAddGame.Size = new Size(150, 35);
            btnAddGame.TabIndex = 3;
            btnAddGame.Text = "Add New Game";
            btnAddGame.UseVisualStyleBackColor = false;
            btnAddGame.Click += btnAddGame_Click;
            // 
            // btnEditGame
            // 
            btnEditGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditGame.BackColor = Color.FromArgb(69, 90, 100);
            btnEditGame.FlatAppearance.BorderSize = 0;
            btnEditGame.FlatStyle = FlatStyle.Flat;
            btnEditGame.ForeColor = Color.White;
            btnEditGame.Location = new Point(185, 460);
            btnEditGame.Name = "btnEditGame";
            btnEditGame.Size = new Size(150, 35);
            btnEditGame.TabIndex = 4;
            btnEditGame.Text = "Edit Selected";
            btnEditGame.UseVisualStyleBackColor = false;
            btnEditGame.Click += btnEditGame_Click;
            // 
            // btnDeleteGame
            // 
            btnDeleteGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDeleteGame.BackColor = Color.FromArgb(120, 50, 50);
            btnDeleteGame.FlatAppearance.BorderSize = 0;
            btnDeleteGame.FlatStyle = FlatStyle.Flat;
            btnDeleteGame.ForeColor = Color.White;
            btnDeleteGame.Location = new Point(345, 460);
            btnDeleteGame.Name = "btnDeleteGame";
            btnDeleteGame.Size = new Size(150, 35);
            btnDeleteGame.TabIndex = 5;
            btnDeleteGame.Text = "Delete Selected";
            btnDeleteGame.UseVisualStyleBackColor = false;
            btnDeleteGame.Click += btnDeleteGame_Click;
            // 
            // btnRefreshGames
            // 
            btnRefreshGames.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRefreshGames.BackColor = Color.FromArgb(69, 90, 100);
            btnRefreshGames.FlatAppearance.BorderSize = 0;
            btnRefreshGames.FlatStyle = FlatStyle.Flat;
            btnRefreshGames.ForeColor = Color.White;
            btnRefreshGames.Location = new Point(785, 460);
            btnRefreshGames.Name = "btnRefreshGames";
            btnRefreshGames.Size = new Size(150, 35);
            btnRefreshGames.TabIndex = 6;
            btnRefreshGames.Text = "Refresh List";
            btnRefreshGames.UseVisualStyleBackColor = false;
            btnRefreshGames.Click += btnRefreshGames_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(120, 50, 50);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(835, 39);
            button1.Name = "button1";
            button1.Size = new Size(100, 25);
            button1.TabIndex = 9;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // VendorMainForm
            // 
            BackColor = Color.FromArgb(30, 30, 40);
            ClientSize = new Size(960, 550);
            Controls.Add(button1);
            Controls.Add(btnRefreshGames);
            Controls.Add(btnDeleteGame);
            Controls.Add(btnEditGame);
            Controls.Add(btnAddGame);
            Controls.Add(dgvMyGames);
            Controls.Add(lblMyGames);
            Controls.Add(lblWelcome);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "VendorMainForm";
            Padding = new Padding(20);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Game Rental System - Vendor Dashboard";
            ((System.ComponentModel.ISupportInitialize)dgvMyGames).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblWelcome;
        private Label lblMyGames;
        private DataGridView dgvMyGames;
        private Button btnAddGame;
        private Button btnEditGame;
        private Button btnDeleteGame;
        private Button btnRefreshGames;
        private Button button1;
    }
}
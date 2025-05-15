namespace GameRentalSystem.UI
{
    partial class AdminMainForm
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            lblWelcome = new Label();
            tabControlAdmin = new TabControl();
            tabPageUsers = new TabPage();
            btnRefreshUsers = new Button();
            btnDeleteUser = new Button();
            btnChangeRole = new Button();
            dgvUsers = new DataGridView();
            tabPageGames = new TabPage();
            btnRefreshGames = new Button();
            btnRejectGame = new Button();
            btnApproveGame = new Button();
            btnDeleteGame = new Button();
            btnEditGame = new Button();
            btnAddGame = new Button();
            dgvGames = new DataGridView();
            tabPageRequests = new TabPage();
            btnRefreshRequests = new Button();
            btnRejectRequest = new Button();
            btnApproveRequest = new Button();
            dgvAdminRequests = new DataGridView();
            tabPageReports = new TabPage();
            dgvReportResults = new DataGridView();
            btnGenerateReport = new Button();
            cmbReportType = new ComboBox();
            lblSelectReport = new Label();
            button1 = new Button();
            tabControlAdmin.SuspendLayout();
            tabPageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            tabPageGames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGames).BeginInit();
            tabPageRequests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAdminRequests).BeginInit();
            tabPageReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReportResults).BeginInit();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(20, 15);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(168, 25);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome, Admin!";
            // 
            // tabControlAdmin
            // 
            tabControlAdmin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlAdmin.Controls.Add(tabPageUsers);
            tabControlAdmin.Controls.Add(tabPageGames);
            tabControlAdmin.Controls.Add(tabPageRequests);
            tabControlAdmin.Controls.Add(tabPageReports);
            tabControlAdmin.Location = new Point(20, 50);
            tabControlAdmin.Name = "tabControlAdmin";
            tabControlAdmin.SelectedIndex = 0;
            tabControlAdmin.Size = new Size(860, 530);
            tabControlAdmin.TabIndex = 1;
            // 
            // tabPageUsers
            // 
            tabPageUsers.BackColor = Color.FromArgb(30, 30, 40);
            tabPageUsers.Controls.Add(btnRefreshUsers);
            tabPageUsers.Controls.Add(btnDeleteUser);
            tabPageUsers.Controls.Add(btnChangeRole);
            tabPageUsers.Controls.Add(dgvUsers);
            tabPageUsers.Location = new Point(4, 26);
            tabPageUsers.Name = "tabPageUsers";
            tabPageUsers.Padding = new Padding(3);
            tabPageUsers.Size = new Size(852, 500);
            tabPageUsers.TabIndex = 0;
            tabPageUsers.Text = "Users";
            // 
            // btnRefreshUsers
            // 
            btnRefreshUsers.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRefreshUsers.BackColor = Color.FromArgb(69, 90, 100);
            btnRefreshUsers.FlatAppearance.BorderSize = 0;
            btnRefreshUsers.FlatStyle = FlatStyle.Flat;
            btnRefreshUsers.ForeColor = Color.White;
            btnRefreshUsers.Location = new Point(6, 467);
            btnRefreshUsers.Name = "btnRefreshUsers";
            btnRefreshUsers.Size = new Size(100, 25);
            btnRefreshUsers.TabIndex = 3;
            btnRefreshUsers.Text = "Refresh";
            btnRefreshUsers.UseVisualStyleBackColor = false;
            btnRefreshUsers.Click += btnRefreshUsers_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDeleteUser.BackColor = Color.FromArgb(120, 50, 50);
            btnDeleteUser.FlatAppearance.BorderSize = 0;
            btnDeleteUser.FlatStyle = FlatStyle.Flat;
            btnDeleteUser.ForeColor = Color.White;
            btnDeleteUser.Location = new Point(746, 467);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(100, 25);
            btnDeleteUser.TabIndex = 2;
            btnDeleteUser.Text = "Delete User";
            btnDeleteUser.UseVisualStyleBackColor = false;
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // btnChangeRole
            // 
            btnChangeRole.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnChangeRole.BackColor = Color.FromArgb(0, 150, 136);
            btnChangeRole.FlatAppearance.BorderSize = 0;
            btnChangeRole.FlatStyle = FlatStyle.Flat;
            btnChangeRole.ForeColor = Color.White;
            btnChangeRole.Location = new Point(640, 467);
            btnChangeRole.Name = "btnChangeRole";
            btnChangeRole.Size = new Size(100, 25);
            btnChangeRole.TabIndex = 1;
            btnChangeRole.Text = "Change Role";
            btnChangeRole.UseVisualStyleBackColor = false;
            btnChangeRole.Click += btnChangeRole_Click;
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsers.BackgroundColor = Color.FromArgb(40, 40, 50);
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.GridColor = Color.FromArgb(60, 60, 70);
            dgvUsers.Location = new Point(6, 6);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 60);
            dgvUsers.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
            dgvUsers.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 107);
            dgvUsers.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(840, 455);
            dgvUsers.TabIndex = 0;
            // 
            // tabPageGames
            // 
            tabPageGames.BackColor = Color.FromArgb(30, 30, 40);
            tabPageGames.Controls.Add(btnRefreshGames);
            tabPageGames.Controls.Add(btnRejectGame);
            tabPageGames.Controls.Add(btnApproveGame);
            tabPageGames.Controls.Add(btnDeleteGame);
            tabPageGames.Controls.Add(btnEditGame);
            tabPageGames.Controls.Add(btnAddGame);
            tabPageGames.Controls.Add(dgvGames);
            tabPageGames.Location = new Point(4, 26);
            tabPageGames.Name = "tabPageGames";
            tabPageGames.Padding = new Padding(3);
            tabPageGames.Size = new Size(852, 500);
            tabPageGames.TabIndex = 1;
            tabPageGames.Text = "Games";
            // 
            // btnRefreshGames
            // 
            btnRefreshGames.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRefreshGames.BackColor = Color.FromArgb(69, 90, 100);
            btnRefreshGames.FlatAppearance.BorderSize = 0;
            btnRefreshGames.FlatStyle = FlatStyle.Flat;
            btnRefreshGames.ForeColor = Color.White;
            btnRefreshGames.Location = new Point(6, 467);
            btnRefreshGames.Name = "btnRefreshGames";
            btnRefreshGames.Size = new Size(100, 25);
            btnRefreshGames.TabIndex = 6;
            btnRefreshGames.Text = "Refresh";
            btnRefreshGames.UseVisualStyleBackColor = false;
            btnRefreshGames.Click += btnRefreshGames_Click;
            // 
            // btnRejectGame
            // 
            btnRejectGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRejectGame.BackColor = Color.FromArgb(120, 50, 50);
            btnRejectGame.FlatAppearance.BorderSize = 0;
            btnRejectGame.FlatStyle = FlatStyle.Flat;
            btnRejectGame.ForeColor = Color.White;
            btnRejectGame.Location = new Point(746, 467);
            btnRejectGame.Name = "btnRejectGame";
            btnRejectGame.Size = new Size(100, 25);
            btnRejectGame.TabIndex = 5;
            btnRejectGame.Text = "Reject";
            btnRejectGame.UseVisualStyleBackColor = false;
            btnRejectGame.Click += btnRejectGame_Click;
            // 
            // btnApproveGame
            // 
            btnApproveGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnApproveGame.BackColor = Color.FromArgb(0, 150, 136);
            btnApproveGame.FlatAppearance.BorderSize = 0;
            btnApproveGame.FlatStyle = FlatStyle.Flat;
            btnApproveGame.ForeColor = Color.White;
            btnApproveGame.Location = new Point(640, 467);
            btnApproveGame.Name = "btnApproveGame";
            btnApproveGame.Size = new Size(100, 25);
            btnApproveGame.TabIndex = 4;
            btnApproveGame.Text = "Approve";
            btnApproveGame.UseVisualStyleBackColor = false;
            btnApproveGame.Click += btnApproveGame_Click;
            // 
            // btnDeleteGame
            // 
            btnDeleteGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDeleteGame.BackColor = Color.FromArgb(120, 50, 50);
            btnDeleteGame.FlatAppearance.BorderSize = 0;
            btnDeleteGame.FlatStyle = FlatStyle.Flat;
            btnDeleteGame.ForeColor = Color.White;
            btnDeleteGame.Location = new Point(534, 467);
            btnDeleteGame.Name = "btnDeleteGame";
            btnDeleteGame.Size = new Size(100, 25);
            btnDeleteGame.TabIndex = 3;
            btnDeleteGame.Text = "Delete";
            btnDeleteGame.UseVisualStyleBackColor = false;
            btnDeleteGame.Click += btnDeleteGame_Click;
            // 
            // btnEditGame
            // 
            btnEditGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditGame.BackColor = Color.FromArgb(69, 90, 100);
            btnEditGame.FlatAppearance.BorderSize = 0;
            btnEditGame.FlatStyle = FlatStyle.Flat;
            btnEditGame.ForeColor = Color.White;
            btnEditGame.Location = new Point(428, 467);
            btnEditGame.Name = "btnEditGame";
            btnEditGame.Size = new Size(100, 25);
            btnEditGame.TabIndex = 2;
            btnEditGame.Text = "Edit";
            btnEditGame.UseVisualStyleBackColor = false;
            btnEditGame.Click += btnEditGame_Click;
            // 
            // btnAddGame
            // 
            btnAddGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAddGame.BackColor = Color.FromArgb(0, 150, 136);
            btnAddGame.FlatAppearance.BorderSize = 0;
            btnAddGame.FlatStyle = FlatStyle.Flat;
            btnAddGame.ForeColor = Color.White;
            btnAddGame.Location = new Point(322, 467);
            btnAddGame.Name = "btnAddGame";
            btnAddGame.Size = new Size(100, 25);
            btnAddGame.TabIndex = 1;
            btnAddGame.Text = "Add New";
            btnAddGame.UseVisualStyleBackColor = false;
            btnAddGame.Click += btnAddGame_Click;
            // 
            // dgvGames
            // 
            dgvGames.AllowUserToAddRows = false;
            dgvGames.AllowUserToDeleteRows = false;
            dgvGames.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGames.BackgroundColor = Color.FromArgb(40, 40, 50);
            dgvGames.BorderStyle = BorderStyle.None;
            dgvGames.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvGames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvGames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGames.GridColor = Color.FromArgb(60, 60, 70);
            dgvGames.Location = new Point(6, 6);
            dgvGames.MultiSelect = false;
            dgvGames.Name = "dgvGames";
            dgvGames.ReadOnly = true;
            dgvGames.RowHeadersVisible = false;
            dgvGames.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 60);
            dgvGames.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
            dgvGames.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 107);
            dgvGames.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvGames.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGames.Size = new Size(840, 455);
            dgvGames.TabIndex = 0;
            // 
            // tabPageRequests
            // 
            tabPageRequests.BackColor = Color.FromArgb(30, 30, 40);
            tabPageRequests.Controls.Add(btnRefreshRequests);
            tabPageRequests.Controls.Add(btnRejectRequest);
            tabPageRequests.Controls.Add(btnApproveRequest);
            tabPageRequests.Controls.Add(dgvAdminRequests);
            tabPageRequests.Location = new Point(4, 26);
            tabPageRequests.Name = "tabPageRequests";
            tabPageRequests.Padding = new Padding(3);
            tabPageRequests.Size = new Size(852, 500);
            tabPageRequests.TabIndex = 2;
            tabPageRequests.Text = "Admin Requests";
            // 
            // btnRefreshRequests
            // 
            btnRefreshRequests.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRefreshRequests.BackColor = Color.FromArgb(69, 90, 100);
            btnRefreshRequests.FlatAppearance.BorderSize = 0;
            btnRefreshRequests.FlatStyle = FlatStyle.Flat;
            btnRefreshRequests.ForeColor = Color.White;
            btnRefreshRequests.Location = new Point(6, 467);
            btnRefreshRequests.Name = "btnRefreshRequests";
            btnRefreshRequests.Size = new Size(100, 25);
            btnRefreshRequests.TabIndex = 3;
            btnRefreshRequests.Text = "Refresh";
            btnRefreshRequests.UseVisualStyleBackColor = false;
            btnRefreshRequests.Click += btnRefreshRequests_Click;
            // 
            // btnRejectRequest
            // 
            btnRejectRequest.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRejectRequest.BackColor = Color.FromArgb(120, 50, 50);
            btnRejectRequest.FlatAppearance.BorderSize = 0;
            btnRejectRequest.FlatStyle = FlatStyle.Flat;
            btnRejectRequest.ForeColor = Color.White;
            btnRejectRequest.Location = new Point(746, 467);
            btnRejectRequest.Name = "btnRejectRequest";
            btnRejectRequest.Size = new Size(100, 25);
            btnRejectRequest.TabIndex = 2;
            btnRejectRequest.Text = "Reject";
            btnRejectRequest.UseVisualStyleBackColor = false;
            btnRejectRequest.Click += btnRejectRequest_Click;
            // 
            // btnApproveRequest
            // 
            btnApproveRequest.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnApproveRequest.BackColor = Color.FromArgb(0, 150, 136);
            btnApproveRequest.FlatAppearance.BorderSize = 0;
            btnApproveRequest.FlatStyle = FlatStyle.Flat;
            btnApproveRequest.ForeColor = Color.White;
            btnApproveRequest.Location = new Point(640, 467);
            btnApproveRequest.Name = "btnApproveRequest";
            btnApproveRequest.Size = new Size(100, 25);
            btnApproveRequest.TabIndex = 1;
            btnApproveRequest.Text = "Approve";
            btnApproveRequest.UseVisualStyleBackColor = false;
            btnApproveRequest.Click += btnApproveRequest_Click;
            // 
            // dgvAdminRequests
            // 
            dgvAdminRequests.AllowUserToAddRows = false;
            dgvAdminRequests.AllowUserToDeleteRows = false;
            dgvAdminRequests.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAdminRequests.BackgroundColor = Color.FromArgb(40, 40, 50);
            dgvAdminRequests.BorderStyle = BorderStyle.None;
            dgvAdminRequests.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvAdminRequests.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dgvAdminRequests.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAdminRequests.GridColor = Color.FromArgb(60, 60, 70);
            dgvAdminRequests.Location = new Point(6, 6);
            dgvAdminRequests.MultiSelect = false;
            dgvAdminRequests.Name = "dgvAdminRequests";
            dgvAdminRequests.ReadOnly = true;
            dgvAdminRequests.RowHeadersVisible = false;
            dgvAdminRequests.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 60);
            dgvAdminRequests.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
            dgvAdminRequests.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 107);
            dgvAdminRequests.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvAdminRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAdminRequests.Size = new Size(840, 455);
            dgvAdminRequests.TabIndex = 0;
            // 
            // tabPageReports
            // 
            tabPageReports.BackColor = Color.FromArgb(30, 30, 40);
            tabPageReports.Controls.Add(dgvReportResults);
            tabPageReports.Controls.Add(btnGenerateReport);
            tabPageReports.Controls.Add(cmbReportType);
            tabPageReports.Controls.Add(lblSelectReport);
            tabPageReports.Location = new Point(4, 26);
            tabPageReports.Name = "tabPageReports";
            tabPageReports.Padding = new Padding(3);
            tabPageReports.Size = new Size(852, 500);
            tabPageReports.TabIndex = 3;
            tabPageReports.Text = "Reports";
            // 
            // dgvReportResults
            // 
            dgvReportResults.AllowUserToAddRows = false;
            dgvReportResults.AllowUserToDeleteRows = false;
            dgvReportResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReportResults.BackgroundColor = Color.FromArgb(40, 40, 50);
            dgvReportResults.BorderStyle = BorderStyle.None;
            dgvReportResults.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvReportResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dgvReportResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReportResults.GridColor = Color.FromArgb(60, 60, 70);
            dgvReportResults.Location = new Point(6, 35);
            dgvReportResults.Name = "dgvReportResults";
            dgvReportResults.ReadOnly = true;
            dgvReportResults.RowHeadersVisible = false;
            dgvReportResults.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 60);
            dgvReportResults.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
            dgvReportResults.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 107);
            dgvReportResults.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvReportResults.Size = new Size(840, 459);
            dgvReportResults.TabIndex = 3;
            // 
            // btnGenerateReport
            // 
            btnGenerateReport.BackColor = Color.FromArgb(0, 150, 136);
            btnGenerateReport.FlatAppearance.BorderSize = 0;
            btnGenerateReport.FlatStyle = FlatStyle.Flat;
            btnGenerateReport.ForeColor = Color.White;
            btnGenerateReport.Location = new Point(267, 6);
            btnGenerateReport.Name = "btnGenerateReport";
            btnGenerateReport.Size = new Size(150, 25);
            btnGenerateReport.TabIndex = 2;
            btnGenerateReport.Text = "Generate Report";
            btnGenerateReport.UseVisualStyleBackColor = false;
            btnGenerateReport.Click += btnGenerateReport_Click;
            // 
            // cmbReportType
            // 
            cmbReportType.BackColor = Color.FromArgb(50, 50, 60);
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.FlatStyle = FlatStyle.Flat;
            cmbReportType.ForeColor = Color.White;
            cmbReportType.FormattingEnabled = true;
            cmbReportType.Location = new Point(100, 6);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(161, 25);
            cmbReportType.TabIndex = 1;
            // 
            // lblSelectReport
            // 
            lblSelectReport.AutoSize = true;
            lblSelectReport.Location = new Point(6, 9);
            lblSelectReport.Name = "lblSelectReport";
            lblSelectReport.Size = new Size(89, 17);
            lblSelectReport.TabIndex = 0;
            lblSelectReport.Text = "Select Report:";
            // 
            // btnLogout
            // 
           
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(120, 50, 50);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(770, 17);
            button1.Name = "button1";
            button1.Size = new Size(100, 25);
            button1.TabIndex = 3;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // AdminMainForm
            // 
            BackColor = Color.FromArgb(30, 30, 40);
            ClientSize = new Size(900, 600);
            Controls.Add(button1);
            Controls.Add(tabControlAdmin);
            Controls.Add(lblWelcome);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AdminMainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin Dashboard - Game Rental System";
            tabControlAdmin.ResumeLayout(false);
            tabPageUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            tabPageGames.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGames).EndInit();
            tabPageRequests.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAdminRequests).EndInit();
            tabPageReports.ResumeLayout(false);
            tabPageReports.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReportResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblWelcome;
        private TabControl tabControlAdmin;
        private TabPage tabPageUsers;
        private TabPage tabPageGames;
        private TabPage tabPageRequests;
        private TabPage tabPageReports;
        private DataGridView dgvUsers;
        private Button btnDeleteUser;
        private Button btnChangeRole;
        private DataGridView dgvGames;
        private Button btnAddGame;
        private Button btnEditGame;
        private Button btnDeleteGame;
        private Button btnApproveGame;
        private Button btnRejectGame;
        private DataGridView dgvAdminRequests;
        private Button btnApproveRequest;
        private Button btnRejectRequest;
        private DataGridView dgvReportResults;
        private Button btnGenerateReport;
        private ComboBox cmbReportType;
        private Label lblSelectReport;
        private Button btnRefreshUsers;
        private Button btnRefreshGames;
        private Button btnRefreshRequests;
        private Button btnLogout;
        private Button button1;
    }
}
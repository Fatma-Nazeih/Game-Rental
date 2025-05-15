namespace GameRentalSystem.UI
{
    partial class AdminRequestForm
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
            dgvAdminRequests = new DataGridView();
            btnApprove = new Button();
            btnReject = new Button();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAdminRequests).BeginInit();
            SuspendLayout();

            // Form Styling
            BackColor = Color.FromArgb(30, 30, 40); // Dark background
            ForeColor = Color.White;
            Font = new Font("Outfit", 10F, FontStyle.Regular);
            Text = "Pending Admin Requests";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(850, 500);

            // dgvAdminRequests
            dgvAdminRequests.AllowUserToAddRows = false;
            dgvAdminRequests.AllowUserToDeleteRows = false;
            dgvAdminRequests.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAdminRequests.BackgroundColor = Color.FromArgb(40, 40, 50);
            dgvAdminRequests.BorderStyle = BorderStyle.None;
            dgvAdminRequests.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvAdminRequests.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.FromArgb(0, 150, 136), // Teal header
                ForeColor = Color.White,
                
            };
            dgvAdminRequests.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAdminRequests.GridColor = Color.FromArgb(60, 60, 70);
            dgvAdminRequests.Location = new Point(20, 20);
            dgvAdminRequests.MultiSelect = false;
            dgvAdminRequests.Name = "dgvAdminRequests";
            dgvAdminRequests.ReadOnly = true;
            dgvAdminRequests.RowHeadersVisible = false;
            dgvAdminRequests.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 60);
            dgvAdminRequests.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
            dgvAdminRequests.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 107);
            dgvAdminRequests.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvAdminRequests.RowTemplate.Height = 25;
            dgvAdminRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAdminRequests.Size = new Size(810, 420);
            dgvAdminRequests.TabIndex = 0;

            // btnApprove
            btnApprove.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnApprove.BackColor = Color.FromArgb(0, 150, 136); // Teal
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.ForeColor = Color.White;
            btnApprove.Location = new Point(650, 450);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(90, 35);
            btnApprove.TabIndex = 1;
            btnApprove.Text = "Approve";
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += btnApprove_Click;

            // btnReject
            btnReject.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnReject.BackColor = Color.FromArgb(120, 50, 50); // Soft red
            btnReject.FlatStyle = FlatStyle.Flat;
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.ForeColor = Color.White;
            btnReject.Location = new Point(750, 450);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(90, 35);
            btnReject.TabIndex = 2;
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = false;
            btnReject.Click += btnReject_Click;

            // btnRefresh
            btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRefresh.BackColor = Color.FromArgb(69, 90, 100); // Dark teal
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(20, 450);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(90, 35);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;

            // AdminRequestForm
            Controls.Add(btnRefresh);
            Controls.Add(btnReject);
            Controls.Add(btnApprove);
            Controls.Add(dgvAdminRequests);
            Name = "AdminRequestForm";
            ((System.ComponentModel.ISupportInitialize)dgvAdminRequests).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvAdminRequests;
        private Button btnApprove;
        private Button btnReject;
        private Button btnRefresh;
    }
}
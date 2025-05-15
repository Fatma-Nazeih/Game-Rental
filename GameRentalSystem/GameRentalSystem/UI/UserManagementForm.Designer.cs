namespace GameRentalSystem.UI
{
    partial class UserManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.lblUserList = new System.Windows.Forms.Label();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.btnChangeRole = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnRefreshUsers = new System.Windows.Forms.Button(); // Optional refresh button
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();
            //
            // lblUserList
            //
            this.lblUserList.AutoSize = true;
            this.lblUserList.Location = new System.Drawing.Point(20, 20);
            this.lblUserList.Name = "lblUserList";
            this.lblUserList.Size = new System.Drawing.Size(86, 17);
            this.lblUserList.TabIndex = 0;
            this.lblUserList.Text = "System Users:";
            //
            // dgvUsers
            //
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(20, 40);
            this.dgvUsers.MultiSelect = false; // Allow only one row selection
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true; // Prevent direct editing in the grid
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; // Select the whole row
            this.dgvUsers.Size = new System.Drawing.Size(760, 350); // Adjust size as needed
            this.dgvUsers.TabIndex = 1;
            //
            // btnChangeRole
            //
            this.btnChangeRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChangeRole.Location = new System.Drawing.Point(20, 400);
            this.btnChangeRole.Name = "btnChangeRole";
            this.btnChangeRole.Size = new System.Drawing.Size(120, 30);
            this.btnChangeRole.TabIndex = 2;
            this.btnChangeRole.Text = "Change Role";
            this.btnChangeRole.UseVisualStyleBackColor = true;
            // Link this event handler in the designer:
            // this.btnChangeRole.Click += new System.EventHandler(this.btnChangeRole_Click);
            //
            // btnDeleteUser
            //
            this.btnDeleteUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteUser.Location = new System.Drawing.Point(150, 400);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(120, 30);
            this.btnDeleteUser.TabIndex = 3;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            // Link this event handler in the designer:
            // this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            //
            // btnRefreshUsers
            //
            this.btnRefreshUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshUsers.Location = new System.Drawing.Point(660, 400);
            this.btnRefreshUsers.Name = "btnRefreshUsers";
            this.btnRefreshUsers.Size = new System.Drawing.Size(120, 30);
            this.btnRefreshUsers.TabIndex = 4;
            this.btnRefreshUsers.Text = "Refresh List";
            this.btnRefreshUsers.UseVisualStyleBackColor = true;
            // Link this event handler in the designer:
            // this.btnRefreshUsers.Click += new System.EventHandler(this.btnRefreshUsers_Click);
            //
            // UserManagementForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(800, 450); // Standard size, adjust if needed
            this.Controls.Add(this.btnRefreshUsers);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.btnChangeRole);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.lblUserList);
            this.Name = "UserManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; // Center relative to parent (AdminMainForm)
            this.Text = "Game Rental System - User Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserList;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Button btnChangeRole;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnRefreshUsers; // Optional refresh button
    }
}

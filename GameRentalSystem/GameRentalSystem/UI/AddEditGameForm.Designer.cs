namespace GameRentalSystem.UI
{
    partial class AddEditGameForm
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
            lblTitle = new Label();
            txtTitle = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblReleaseYear = new Label();
            txtReleaseYear = new TextBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblVendor = new Label();
            cmbVendor = new ComboBox();
            lblIsApproved = new Label();
            chkIsApproved = new CheckBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();

            // Form Styling
            BackColor = Color.FromArgb(30, 30, 40); // Dark background
            ForeColor = Color.White;
            Font = new Font("Outfit", 10F, FontStyle.Regular);
            Text = "Add/Edit Game";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(450, 450);
            Padding = new Padding(25);

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(35, 17);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Title:";

            // txtTitle
            txtTitle.BackColor = Color.FromArgb(50, 50, 60);
            txtTitle.BorderStyle = BorderStyle.FixedSingle;
            txtTitle.ForeColor = Color.White;
            txtTitle.Location = new Point(150, 27);
            txtTitle.Size = new Size(250, 25);
            txtTitle.TabIndex = 1;

            // lblDescription
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(30, 70);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(76, 17);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description:";

            // txtDescription
            txtDescription.BackColor = Color.FromArgb(50, 50, 60);
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.ForeColor = Color.White;
            txtDescription.Location = new Point(150, 67);
            txtDescription.Multiline = true;
            txtDescription.Size = new Size(250, 100);
            txtDescription.TabIndex = 3;

            // lblReleaseYear
            lblReleaseYear.AutoSize = true;
            lblReleaseYear.Location = new Point(30, 180);
            lblReleaseYear.Name = "lblReleaseYear";
            lblReleaseYear.Size = new Size(83, 17);
            lblReleaseYear.TabIndex = 4;
            lblReleaseYear.Text = "Release Year:";

            // txtReleaseYear
            txtReleaseYear.BackColor = Color.FromArgb(50, 50, 60);
            txtReleaseYear.BorderStyle = BorderStyle.FixedSingle;
            txtReleaseYear.ForeColor = Color.White;
            txtReleaseYear.Location = new Point(150, 177);
            txtReleaseYear.Size = new Size(120, 25);
            txtReleaseYear.TabIndex = 5;

            // lblPrice
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(30, 220);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(40, 17);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Price:";

            // txtPrice
            txtPrice.BackColor = Color.FromArgb(50, 50, 60);
            txtPrice.BorderStyle = BorderStyle.FixedSingle;
            txtPrice.ForeColor = Color.White;
            txtPrice.Location = new Point(150, 217);
            txtPrice.Size = new Size(120, 25);
            txtPrice.TabIndex = 7;

            // lblCategory
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(30, 260);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(64, 17);
            lblCategory.TabIndex = 8;
            lblCategory.Text = "Category:";

            // cmbCategory
            cmbCategory.BackColor = Color.FromArgb(50, 50, 60);
            cmbCategory.FlatStyle = FlatStyle.Flat;
            cmbCategory.ForeColor = Color.White;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(150, 257);
            cmbCategory.Size = new Size(180, 25);
            cmbCategory.TabIndex = 9;

            // lblVendor
            lblVendor.AutoSize = true;
            lblVendor.Location = new Point(30, 300);
            lblVendor.Name = "lblVendor";
            lblVendor.Size = new Size(53, 17);
            lblVendor.TabIndex = 10;
            lblVendor.Text = "Vendor:";

            // cmbVendor
            cmbVendor.BackColor = Color.FromArgb(50, 50, 60);
            cmbVendor.FlatStyle = FlatStyle.Flat;
            cmbVendor.ForeColor = Color.White;
            cmbVendor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVendor.FormattingEnabled = true;
            cmbVendor.Location = new Point(150, 297);
            cmbVendor.Size = new Size(180, 25);
            cmbVendor.TabIndex = 11;

            // lblIsApproved
            lblIsApproved.AutoSize = true;
            lblIsApproved.Location = new Point(30, 340);
            lblIsApproved.Name = "lblIsApproved";
            lblIsApproved.Size = new Size(80, 17);
            lblIsApproved.TabIndex = 12;
            lblIsApproved.Text = "Is Approved:";

            // chkIsApproved
            chkIsApproved.AutoSize = true;
            chkIsApproved.FlatStyle = FlatStyle.Flat;
            chkIsApproved.ForeColor = Color.White;
            chkIsApproved.Location = new Point(150, 340);
            chkIsApproved.Size = new Size(15, 14);
            chkIsApproved.TabIndex = 13;
            chkIsApproved.UseVisualStyleBackColor = false;

            // btnSave
            btnSave.BackColor = Color.FromArgb(0, 150, 136); // Teal
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(150, 380);
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 14;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            // btnCancel
            btnCancel.BackColor = Color.FromArgb(69, 90, 100); // Dark teal
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(260, 380);
            btnCancel.Size = new Size(100, 35);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;

            // AddEditGameForm
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(chkIsApproved);
            Controls.Add(lblIsApproved);
            Controls.Add(cmbVendor);
            Controls.Add(lblVendor);
            Controls.Add(cmbCategory);
            Controls.Add(lblCategory);
            Controls.Add(txtPrice);
            Controls.Add(lblPrice);
            Controls.Add(txtReleaseYear);
            Controls.Add(lblReleaseYear);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(txtTitle);
            Controls.Add(lblTitle);
            Name = "AddEditGameForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtTitle;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblReleaseYear;
        private TextBox txtReleaseYear;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblVendor;
        private ComboBox cmbVendor;
        private Label lblIsApproved;
        private CheckBox chkIsApproved;
        private Button btnSave;
        private Button btnCancel;
    }
}
namespace GameRentalSystem.UI
{
    partial class ReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.lblSelectReport = new System.Windows.Forms.Label();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.dgvReportResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportResults)).BeginInit();
            this.SuspendLayout();
            //
            // lblSelectReport
            //
            this.lblSelectReport.AutoSize = true;
            this.lblSelectReport.Location = new System.Drawing.Point(20, 20);
            this.lblSelectReport.Name = "lblSelectReport";
            this.lblSelectReport.Size = new System.Drawing.Size(74, 13);
            this.lblSelectReport.TabIndex = 0;
            this.lblSelectReport.Text = "Select Report:";
            //
            // cmbReportType
            //
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; // Prevent typing
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Location = new System.Drawing.Point(100, 17);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(250, 21);
            this.cmbReportType.TabIndex = 1;
            //
            // btnGenerateReport
            //
            this.btnGenerateReport.Location = new System.Drawing.Point(360, 15);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(110, 23);
            this.btnGenerateReport.TabIndex = 2;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            // Link this event handler in the designer:
            // this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            //
            // dgvReportResults
            //
            this.dgvReportResults.AllowUserToAddRows = false;
            this.dgvReportResults.AllowUserToDeleteRows = false;
            this.dgvReportResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReportResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportResults.Location = new System.Drawing.Point(20, 50);
            this.dgvReportResults.Name = "dgvReportResults";
            this.dgvReportResults.ReadOnly = true;
            this.dgvReportResults.Size = new System.Drawing.Size(760, 380); // Adjust size as needed
            this.dgvReportResults.TabIndex = 3;
            //
            // ReportForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(800, 450); // Standard size, adjust if needed
            this.Controls.Add(this.dgvReportResults);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.cmbReportType);
            this.Controls.Add(this.lblSelectReport);
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; // Center relative to parent (AdminMainForm)
            this.Text = "Game Rental System - Reports";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectReport;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.DataGridView dgvReportResults;
    }
}
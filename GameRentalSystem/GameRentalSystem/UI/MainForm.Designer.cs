namespace GameRentalSystem.UI
{
    partial class MainForm
    {
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
            this.components = new System.ComponentModel.Container();

            // Set a minimal size, though it won't be visible
            this.ClientSize = new System.Drawing.Size(100, 50);
            this.Text = "Loading..."; // Or some brief title
            this.ControlBox = false; // Hide minimize/maximize/close buttons
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // Hide border
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; // Center it briefly

        }

        #endregion
    }
}
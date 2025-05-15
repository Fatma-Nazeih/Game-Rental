using GameRentalSystem.UI;
using System;
using System.Windows.Forms;

namespace GameRentalSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm()); // Start with the Login Form
        }
    }
}
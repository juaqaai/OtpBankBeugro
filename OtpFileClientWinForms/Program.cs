using System;
using System.Windows.Forms;

namespace OtpFileClientWinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ClientForm());
            }
            catch (Exception e)
            {
                MessageBox.Show($"Hiba történt: {e.Message} \r\n részletek: {e.StackTrace}", "Hiba");
            }
        }
    }
}

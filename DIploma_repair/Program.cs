using System;
using System.Windows.Forms;

namespace DIploma_repair
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DIploma_repair.LogIn.LogIn()) ;
        }
    }
}

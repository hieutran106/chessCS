using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessCS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ChessBoard cb = new ChessBoard();
            cb.Reset();
            cb.PrintBoard();
            Console.WriteLine("=============");
            Console.WriteLine(cb.GetMove(0, 5, 0, 6));
            Console.WriteLine(cb.GetMove(0, 5, 1, 6));

            
            //Show main form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //create main form
            MainForm mainForm = new MainForm();
            mainForm.ChessBoard = cb;
            Application.Run(mainForm);
        }
    }
}

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
            //cb.Load("8/8/p7/8/8/2n1P3/8/8 w 3");

            //Show main form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //create main form
            MainForm mainForm = new MainForm();
            mainForm.ChessBoard = cb;
            Rating rating = new Rating();
            //test alpha beta
            //Console.WriteLine(cb.alphaBeta(4, 1000000, -1000000, null, false));
            Application.Run(mainForm);
        }
    }
}

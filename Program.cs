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
            //cb.Load("rnbqk2r/ppp1bppp/4pn2/3p4/3P4/N1P1B3/PP2PPPP/R2QKBNR w KQkq d6 0 5");

            //Show main form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //create main form
            MainForm mainForm = new MainForm();
            mainForm.ChessBoard = cb;
            //test alpha beta
            //Console.WriteLine(cb.alphaBeta(4, 1000000, -1000000, null, false));
            Application.Run(mainForm);
        }
    }
}

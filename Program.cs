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
            //cb.Load("1q6/8/p7/8/8/2n3P1/4P3/5N2 w 3");
            //cb.Load("8/6P1/p7/8/8/2n5/4P3/5N2 w 3");
            //cb.Load("2r5/1r6/8/8/8/8/K7/8 w 3");
            //cb.Load("2r5/1r6/8/8/2N5/8/8/8 w 3");
            cb.Load("8/3q4/4P3/1p3k2/4K1P1/8/R7/8 w 3");
            //Show main form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //create main form
            MainForm mainForm = new MainForm(cb);
            //mainForm.ChessBoard = cb;
            Evaluation rating = new Evaluation();
            //test alpha beta
            //Console.WriteLine(cb.alphaBeta(4, 1000000, -1000000, null, false));
            Application.Run(mainForm);
        }
    }
}

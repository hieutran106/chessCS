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
            Console.WriteLine(ChessBoard.PositionFromCoordinate(0, 0));
            Console.WriteLine(ChessBoard.PositionFromCoordinate(7, 7));
            Console.WriteLine(ChessBoard.PositionFromCoordinate(0, 4));
            Console.WriteLine(ChessBoard.PositionFromCoordinate(7, 5));
            Console.WriteLine("=============");
            Console.WriteLine(cb.GetMove(0, 5, 0, 6));
            Console.WriteLine(cb.GetMove(0, 5, 1, 6));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

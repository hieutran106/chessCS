using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessCS
{
    public partial class MainForm : Form
    {
        
        private ChessBoard chessBoard;

        public ChessBoard ChessBoard {
            get
            {
                return chessBoard;
            }
            set
            {
                chessBoard = value;
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        boardGUI[i, j].Piece = chessBoard.Board[i, j];
            }
        }
        private SquareBox[,] boardGUI;
        public MainForm()
        {
            InitializeComponent();

            //init board GUI
            boardGUI = new SquareBox[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    boardGUI[i, j] = new SquareBox(i, j);
                    this.Controls.Add(boardGUI[i, j]);
                }
                    

        }
    }
}

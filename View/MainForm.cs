using ChessCS.ChessPieces;
using ChessCS.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessCS
{
    public partial class MainForm : Form
    {
        
        private ChessBoard chessBoard;
        private bool isSelected;
        private int x_select, y_select;
        private List<Move> possibleMoves;
        private bool isSearchingForBestMove;
        //history
        private Stack<Move> moveHistory;
       
        #region GUI
        private ChessBoardControl chessBoardControl;
        #endregion
        public ChessBoard ChessBoard {
            get
            {
                return chessBoard;
            }
            set
            {
                chessBoard = value;
                infoLabel.Text = $"Match - ActiveColor: {(chessBoard.ActiveColor?"white":"black")} fullMove:{chessBoard.FullMove}";               
            }
        }

        
        public MainForm()
        {
            InitializeComponent();
                            
        }
        public MainForm(ChessBoard chessBoard)
        {
            InitializeComponent();
            chessBoardControl = new ChessBoardControl(chessBoard);
            chessBoardControl.MouseUp += new MouseEventHandler(this.ChessBoardControl_Click);
            this.Controls.Add(chessBoardControl);
            this.ChessBoard = chessBoard;
            //move history
            moveHistory = new Stack<Move>();

        }

        //Put a piece
        public void Put_Piece(int x,int y, char piece)
        {
            chessBoard.Board[x, y] = piece;
            //rehash
            chessBoard.UpdateHash();
            chessBoardControl.Invalidate();
        }
        private List<Move> PossibleMove(int x,int y)
        {
            List<Move> moves = new List<Move>();
            char piece = char.ToUpper(chessBoard.Board[x_select, y_select]);
            switch (piece)
            {
                case 'P':
                    moves = Pawn.generateMove(x_select, y_select, chessBoard);
                    break;
                case 'R':
                    moves = Rook.generateMove(x_select, y_select, chessBoard);
                    break;
                case 'N':
                    moves = Knight.generateMove(x_select, y_select, chessBoard);
                    break;
                case 'B':
                    moves = Bishop.generateMove(x_select, y_select, chessBoard);
                    break;
                case 'Q':
                    moves = Queen.generateMove(x_select, y_select, chessBoard);
                    break;
                case 'K':
                    moves = King.generateMove(x_select, y_select, chessBoard);
                    break;
                default:
                    break;
            }
            return moves;

        }
        private void ChessBoardControl_Click(object sender, MouseEventArgs e)
        {
            if (isSearchingForBestMove)
                return;
            int col = e.Location.X / ChessBoardControl.SIZE;
            int row = e.Location.Y / ChessBoardControl.SIZE;
            if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine($"Click at [{row},{col}]");
                if (chessBoard.Board[row, col] != '.' && isSelected == false) //Click on a chess piece
                {
                    bool clickColor = char.IsUpper(chessBoard.Board[row, col]);
                    if (clickColor == chessBoard.ActiveColor)
                    {
                        isSelected = true;
                        x_select = row;
                        y_select = col;
                        //show border for current row
                        chessBoardControl.HighlighCell(new Point(row, col));
                        //show border for possible move
                        possibleMoves = PossibleMove(x_select, y_select);
                        chessBoardControl.HighlighCells(possibleMoves);
                    }
                    else
                    {
                        //It's BLACK turn
                        Console.WriteLine("Wrong active color");
                    }
                } else if (isSelected ==true)
                {
                    chessBoardControl.RemoveHighlightCells();
                    //remove hightlight in possible move
                    foreach (Move move in possibleMoves)
                    {
                        int xDst = move.X_Des;
                        int yDst = move.Y_Des;
                        if (row == xDst && col == yDst)
                        {
                            //set animation
                            //Player Move
                            MakeMove(x_select, y_select, xDst, yDst);
                            break;
                        }
                    }
                    CancelCurrentSelection();
                }
            } else
            {
                //Right click
                AddPieceForm form = new AddPieceForm(row, col, this);
                form.Show();
            }
        }
        
        private void CancelCurrentSelection()
        {
            isSelected = false;
            x_select = -1;
            y_select = -1;
        }
        public async void MakeMove(int x_src,int y_src, int x_des,int y_des)
        {
            Move move = chessBoard.GetMove(x_src, y_src, x_des, y_des);
            chessBoard.MakeMove(move);
            moveHistory.Push(move);
            chessBoardControl.Invalidate();
            //Update GUI
            //Update info
            infoLabel.Text = $"Match - ActiveColor: {(chessBoard.ActiveColor ? "white" : "black")} fullMove:{chessBoard.FullMove}";
            //
            if (chessBoard.ActiveColor==ChessBoard.BLACK)
            {
                moveHistoryTextBox.AppendText($"\n{chessBoard.FullMove}. {move}");
            } else
            {
                moveHistoryTextBox.AppendText($"  {move}");
            }

            if (this.chessBoard.ActiveColor==ChessBoard.BLACK)
            {
                //Computer turn
                thinkLabel.Text = "Computer is thinking....";
                isSearchingForBestMove = true;
                Task<Move> computerMoveTask = Task.Run(() => chessBoard.GetAIMove());

                //wait for Task
                await computerMoveTask;
                Move computerMove = computerMoveTask.Result;
                thinkLabel.Text = "";
                isSearchingForBestMove = false;
                //
                MakeMove(computerMove.X_Src, computerMove.Y_Src, computerMove.X_Des, computerMove.Y_Des);
            }

        }
        

        private void getFENBtn_Click(object sender, EventArgs e)
        {
            logTextBox.Clear();
            logTextBox.AppendText(chessBoard.GetFEN());
            logTextBox.AppendText("\r\nHash: " + chessBoard.Hash);
        }

        private void coordinateToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = coordinateToolStripMenuItem.Checked;
            chessBoardControl.ShowCoordinate = isChecked;

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chessBoard.Reset();
            chessBoardControl.Invalidate();
            //chessBoard.Load("rnbqk2r/ppp1bppp/4pn2/3p4/3P4/N1P1B3/PP2PPPP/R2QKBNR w KQkq d6 0 5");
            //reset
            isSelected=false;
            x_select = -1;
            y_select = -1;
            possibleMoves=null;

    }

        private void backBtn_Click(object sender, EventArgs e)
        {
            Move move = moveHistory.Pop();
            chessBoard.UndoMove(move);

            //Update GUI
            chessBoardControl.Invalidate();
            //Update info
            infoLabel.Text = $"Match - ActiveColor: {(chessBoard.ActiveColor ? "white" : "black")} fullMove:{chessBoard.FullMove}";
            //remove text in rich text box
            StringBuilder text = new StringBuilder(moveHistoryTextBox.Text);
            
            if (chessBoard.ActiveColor == ChessBoard.BLACK)
            {
                text.Remove(text.Length - 24, 24);
                moveHistoryTextBox.Text = text.ToString();
            }
            else
            {
                text.Remove(text.Length - 22, 22);
                moveHistoryTextBox.Text = text.ToString();
            }
        }

    }
}

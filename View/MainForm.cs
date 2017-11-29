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
        //history
        private Stack<Move> moveHistory;
       
        #region GUI
        private ChessBoardControl chessBoardControl;
        private Timer animationTimer = new Timer();
        private int animationTick = 0;
        private int x_move_src;
        private int y_move_src;
        private int x_move_dst;
        private int y_move_dst;
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
            //update GUI
            //chessPiecesGUI[x, y].Piece = piece;
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
        private async void ChessBoardControl_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                
                int col = e.Location.X / ChessBoardControl.SIZE;
                int row = e.Location.Y / ChessBoardControl.SIZE;
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
                    bool success = false;
                    foreach (Move move in possibleMoves)
                    {
                        int xDst = move.X_Des;
                        int yDst = move.Y_Des;
                        //boardGUI[x_highlight, y_highlight].IsHighlight = false;
                        if (row == xDst && col == yDst)
                        {
                            //set animation
                            //Player Move

                            animationTimer.Interval = 200;
                            animationTimer.Tick += new EventHandler(TimerEventProcessor);
                            animationTimer.Start();
                            animationTick = 0;
                            x_move_src = x_select;
                            y_move_src = y_select;
                            x_move_dst = xDst;
                            y_move_dst = yDst;
                            this.chessBoardControl.SetAnimation(x_select, y_select, xDst, yDst);
                            break;
                        }
                    }
                    CancelCurrentSelection();



                    //If a valid move, let computer take action
                    //if (x_select != row || y_select != col)
                    //{
                    //    CancelCurrentSelection();
                    //    //computer move
                    //    thinkLabel.Text = "Computer is thinking....";
                    //    Task<Move> computerMoveTask = Task.Run(() => chessBoard.GetAIMove());

                    //    //wait for Task
                    //    await computerMoveTask;
                    //    Move computerMove = computerMoveTask.Result;
                    //    thinkLabel.Text = "";
                    //    //
                    //    MakeMove(computerMove.X_Src, computerMove.Y_Src, computerMove.X_Des, computerMove.Y_Des);
                    //}
                }
            }
        }
        private void TimerEventProcessor(Object myObject,
                                            EventArgs myEventArgs)
        {
            int SIZE = 60;
            int dx = animationTick * (y_move_dst - y_move_src) * SIZE / 10;
            int dy = animationTick * (x_move_dst - y_move_dst) * SIZE / 10;
            int x_ani = y_move_src * SIZE - dx;
            int y_ani = x_move_src * SIZE - dy;
            this.chessBoardControl.UpdateAnimationPosition(x_ani,y_ani);
            Console.WriteLine("Tick: " + animationTick);
            animationTick++;
            if (animationTick == 10)
            {
                animationTick = 0;
                animationTimer.Stop();
                MakeMove(x_move_src, y_move_src, x_move_dst, y_move_dst);
            }
        }
        private void CancelCurrentSelection()
        {
            isSelected = false;
            x_select = -1;
            y_select = -1;
        }
        public void MakeMove(int x_src,int y_src, int x_des,int y_des)
        {
            Move move = chessBoard.GetMove(x_src, y_src, x_des, y_des);
            chessBoard.MakeMove(move);
            moveHistory.Push(move);

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
            

        }
        //Context Menu
        private void AddPieceItem_Click(object sender, EventArgs e)
        {
            //MenuItem menuItem = sender as MenuItem;

            //if (menuItem != null)
            //{
            //    ContextMenu contextMenu = menuItem.GetContextMenu();
            //    //Get picture box
            //    Control sourceControl = contextMenu.SourceControl;
            //    SquareBox squareBox = sourceControl as SquareBox;
            //    if (squareBox != null)
            //    {
            //        //remove piece
            //        //get position
            //        int x = squareBox.X;
            //        int y = squareBox.Y;
            //        //update
            //        AddPieceForm form = new AddPieceForm(x,y,this);
            //        form.Show();
            //    }
            //}

        }

        private void getFENBtn_Click(object sender, EventArgs e)
        {
            logTextBox.AppendText(chessBoard.GetFEN());
        }

        private void coordinateToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = coordinateToolStripMenuItem.Checked;
            //for (int i = 0; i < 8; i++)
            //    for (int j = 0; j < 8; j++)
            //        boardGUI[i, j].ShowCoordinate = isChecked;
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chessBoard.Reset();
            chessBoard.Load("rnbqk2r/ppp1bppp/4pn2/3p4/3P4/N1P1B3/PP2PPPP/R2QKBNR w KQkq d6 0 5");
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
            //chessPiecesGUI[move.X_Src, move.Y_Src].Piece = chessBoard.Board[move.X_Src, move.Y_Src];
            //chessPiecesGUI[move.X_Des, move.Y_Des].Piece = chessBoard.Board[move.X_Des, move.Y_Des];
            //Update info
            infoLabel.Text = $"Match - ActiveColor: {(chessBoard.ActiveColor ? "white" : "black")} fullMove:{chessBoard.FullMove}";
            //remove text in rich text box
            StringBuilder text = new StringBuilder(moveHistoryTextBox.Text);
            
            if (chessBoard.ActiveColor == ChessBoard.BLACK)
            {
                text.Remove(text.Length - 24, 24);
                moveHistoryTextBox.Text = text.ToString();
                //moveHistoryTextBox.AppendText($"\n{chessBoard.Fullmove}. {move}");
            }
            else
            {
                text.Remove(text.Length - 22, 22);
                moveHistoryTextBox.Text = text.ToString();
                //moveHistoryTextBox.AppendText($"  {move}");
            }
        }

        private void RemovePieceItem_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
          
            //if (menuItem!=null)
            //{
            //    ContextMenu contextMenu = menuItem.GetContextMenu();
            //    //Get picture box
            //    Control sourceControl = contextMenu.SourceControl;
            //    SquareBox squareBox = sourceControl as SquareBox;
            //    if (squareBox!=null)
            //    {
            //        //remove piece
            //        //get position
            //        int x = squareBox.X;
            //        int y = squareBox.Y;
            //        //remove a chess piece
            //        Put_Piece(x, y, '.');
            //    }
                
                
            //}
            
        }

    }
}

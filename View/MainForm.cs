using ChessCS.ChessPieces;
using ChessCS.View;
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
        private bool isSelected;
        private int x_select, y_select;
        private List<Move> possibleMoves;
        //history
        private Stack<Move> moveHistory;

        #region GUI
        private SquareBox[,] boardGUI;
        private ChessPieceGUI[,] chessPiecesGUI;
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
                //init board GUI           
                chessPiecesGUI = new ChessPieceGUI[8, 8];
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        
                        if (chessBoard.Board[i,j]!='.')
                        {
                            ChessPieceGUI piece = new ChessPieceGUI(i, j);
                            chessPiecesGUI[i, j] = piece;
                            chessPiecesGUI[i, j].Piece = chessBoard.Board[i, j];
                            
                           // this.Controls.Add(piece);
                            //piece.BringToFront();
                            

                        }
                        
                    }
                boardGUI = new SquareBox[8, 8];
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        SquareBox squareBox = new SquareBox(i, j);
                        
                        boardGUI[i, j] = squareBox;//important



                        ContextMenu mnuContextMenu = new ContextMenu();
                        squareBox.ContextMenu = mnuContextMenu;

                        MenuItem addPieceItem = new MenuItem();
                        addPieceItem.Text = "Add a piece";
                        // Add functionality to the menu items using the Click event. 
                        addPieceItem.Click += new System.EventHandler(this.AddPieceItem_Click);
                        MenuItem removePieceitem = new MenuItem();
                        removePieceitem.Text = "Remove piece";
                        removePieceitem.Click += new System.EventHandler(this.RemovePieceItem_Click);

                        squareBox.ContextMenu.MenuItems.Add(addPieceItem);
                        squareBox.ContextMenu.MenuItems.Add(removePieceitem);

                        //this.Controls.Add(squareBox);
                        
                    }
                foreach (var ele in this.Controls)
                {
                    Console.WriteLine(ele);
                }
                        
            }
        }
        
        public MainForm()
        {
            InitializeComponent();



            chessBoardControl = new ChessBoardControl();
            chessBoardControl.MouseUp += new MouseEventHandler(this.ChessBoardControl_Click);
            this.Controls.Add(chessBoardControl);
            //move history
            moveHistory = new Stack<Move>();
                            
        }
        //Put a piece
        public void Put_Piece(int x,int y, char piece)
        {
            chessBoard.Board[x, y] = piece;
            //update GUI
            chessPiecesGUI[x, y].Piece = piece;
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
                    if (possibleMoves != null)
                    {
                        foreach (Move move in possibleMoves)
                        {
                            int x_highlight = move.X_Des;
                            int y_highlight = move.Y_Des;
                            boardGUI[x_highlight, y_highlight].IsHighlight = false;
                            if (row == x_highlight && col == y_highlight)
                            {
                                //Player Move
                                MakeMove(x_select, y_select, row, col);
                                break;
                            }
                        }
                        possibleMoves = null;
                    }

                    //If a valid move, let computer take action
                    if (x_select != row || y_select != col)
                    {
                        CancelCurrentSelection();
                        //computer move
                        thinkLabel.Text = "Computer is thinking....";
                        Task<Move> computerMoveTask = Task.Run(() => chessBoard.GetAIMove());

                        //wait for Task
                        await computerMoveTask;
                        Move computerMove = computerMoveTask.Result;
                        thinkLabel.Text = "";
                        //
                        MakeMove(computerMove.X_Src, computerMove.Y_Src, computerMove.X_Des, computerMove.Y_Des);
                    }
                    else
                    {
                        CancelCurrentSelection();
                    }
                }
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
            chessPiecesGUI[x_src, y_src].Piece = chessBoard.Board[x_src, y_src];
            chessPiecesGUI[x_des, y_des].Piece = chessBoard.Board[x_des, y_des];
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
            MenuItem menuItem = sender as MenuItem;

            if (menuItem != null)
            {
                ContextMenu contextMenu = menuItem.GetContextMenu();
                //Get picture box
                Control sourceControl = contextMenu.SourceControl;
                SquareBox squareBox = sourceControl as SquareBox;
                if (squareBox != null)
                {
                    //remove piece
                    //get position
                    int x = squareBox.X;
                    int y = squareBox.Y;
                    //update
                    AddPieceForm form = new AddPieceForm(x,y,this);
                    form.Show();
                }
            }

        }

        private void getFENBtn_Click(object sender, EventArgs e)
        {
            logTextBox.AppendText(chessBoard.GetFEN());
        }

        private void coordinateToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = coordinateToolStripMenuItem.Checked;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    boardGUI[i, j].ShowCoordinate = isChecked;
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
            chessPiecesGUI[move.X_Src, move.Y_Src].Piece = chessBoard.Board[move.X_Src, move.Y_Src];
            chessPiecesGUI[move.X_Des, move.Y_Des].Piece = chessBoard.Board[move.X_Des, move.Y_Des];
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
          
            if (menuItem!=null)
            {
                ContextMenu contextMenu = menuItem.GetContextMenu();
                //Get picture box
                Control sourceControl = contextMenu.SourceControl;
                SquareBox squareBox = sourceControl as SquareBox;
                if (squareBox!=null)
                {
                    //remove piece
                    //get position
                    int x = squareBox.X;
                    int y = squareBox.Y;
                    //remove a chess piece
                    Put_Piece(x, y, '.');
                }
                
                
            }
            
        }

    }
}

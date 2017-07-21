using ChessCS.ChessPieces;
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
                    SquareBox squareBox = new SquareBox(i, j);
                    squareBox.MouseUp += new MouseEventHandler(this.SquareBox_Click);
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

                    this.Controls.Add(squareBox);
                }
                    

        }
        //Put a piece
        public void Put_Piece(int x,int y, char piece)
        {
            chessBoard.Board[x, y] = piece;
            boardGUI[x, y].Piece = piece;
        }
        private void SquareBox_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SquareBox p = (SquareBox)sender;
                if (chessBoard.Board[p.X, p.Y] != '.' && isSelected == false) //Click on a chess piece
                {
                    
                    isSelected = true;
                    x_select = p.X;
                    y_select = p.Y;
                    Console.WriteLine($"Selected {chessBoard.Board[x_select, y_select]} at [{p.X},{p.Y}]");
                    //show border
                    boardGUI[x_select, y_select].IsHightlight = true;
                    //show border for possible move
                    char piece = char.ToUpper(chessBoard.Board[x_select, y_select]);
                    switch (piece)
                    {
                        case 'P':
                            possibleMoves = Pawn.generateMove(x_select, y_select, chessBoard);
                            break;
                        case 'R':
                            possibleMoves = Rook.generateMove(x_select, y_select, chessBoard);
                            break;
                        case 'N':
                            possibleMoves = Knight.generateMove(x_select, y_select, chessBoard);
                            break;
                        case 'B':
                            possibleMoves = Bishop.generateMove(x_select, y_select, chessBoard);
                            break;
                        case 'Q':
                            possibleMoves = Queen.generateMove(x_select, y_select, chessBoard);
                            break;
                        case 'K':
                            possibleMoves = King.generateMove(x_select, y_select, chessBoard);
                            break;
                        default:
                            break;
                    }
                    if (possibleMoves != null)
                    {
                        
                        foreach (Move move in possibleMoves)
                        {
                            int x_des = move.Des_X;
                            int y_des = move.Des_Y;                            
                            boardGUI[x_des, y_des].IsHightlight = true;
                        }
                    }


                }
                else if (isSelected == true)
                {
                    Console.WriteLine($"De_select at [{x_select},{y_select}]");
                    isSelected = false;
                    boardGUI[x_select, y_select].IsHightlight = false;
                    x_select = -1;
                    y_select = -1;
                    
                    //remove hightlight in possible move
                    if (possibleMoves != null)
                    {
                        foreach (Move move in possibleMoves)
                        {
                            int x_des = move.Des_X;
                            int y_des = move.Des_Y;
                            boardGUI[x_des, y_des].IsHightlight = false;
                        }
                        possibleMoves = null;
                    }

                }
            }
        }
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

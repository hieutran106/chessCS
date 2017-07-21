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
        private int x_seclect, y_select;
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
            if (e.Button==MouseButtons.Left)
            {
                SquareBox p = (SquareBox)sender;
                Console.WriteLine($"Click on squareBox[{p.X},{p.Y}]");
                if (chessBoard.Board[p.X,p.Y]!='.' && isSelected == false) //Click on a chess piece
                {                  
                        isSelected = true;
                        x_seclect = p.X;
                        y_select = p.Y;
                        //show border
                        boardGUI[x_seclect, y_select].IsHightlight = true;
                        //show border for possible move
                        if (char.IsLower(chessBoard.Board[x_seclect,y_select])) // is black piece
                    {
                        Console.WriteLine("Show possible moves for black piece");
                        switch (chessBoard.Board[x_seclect,y_select])
                        {
                            case 'p':
                                possibleMoves=Pawn.generateMove(x_seclect, y_select, chessBoard);
                                break;
                            case 'r':
                                possibleMoves = Rook.generateMove(x_seclect, y_select, chessBoard);
                                break;
                            case 'n':
                                possibleMoves = Knight.generateMove(x_seclect, y_select, chessBoard);
                                break;
                            case 'b':
                                possibleMoves = Bishop.generateMove(x_seclect, y_select, chessBoard);
                                break;
                            case 'q':
                                possibleMoves = Queen.generateMove(x_seclect, y_select, chessBoard);
                                break;
                            case 'k':
                                possibleMoves = King.generateMove(x_seclect, y_select, chessBoard);
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
                                Console.WriteLine($"Hightlight [{x_des},{y_des}]");
                                boardGUI[x_seclect, y_select].IsHightlight = true;
                            }
                        }
                    }

                } else if (isSelected==true) {
                    isSelected = false;
                    boardGUI[x_seclect, y_select].IsHightlight = false;
                    x_seclect = -1;
                    y_select = -1;

                    //remove hightlight in possible move
                    if (possibleMoves!=null)
                    {
                        foreach (Move move in possibleMoves)
                        {
                            int x_des = move.Des_X;
                            int y_des = move.Des_Y;
                            boardGUI[x_seclect, y_select].IsHightlight = false;
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

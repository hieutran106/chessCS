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
        private void SquareBox_Click(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                SquareBox p = (SquareBox)sender;
                Console.WriteLine($"Click on squareBox[{p.X},{p.Y}]");
            }
        }
        private void AddPieceItem_Click(object sender, EventArgs e)
        {

            Console.WriteLine($"sender: {sender}, event: {e}");


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
                    //update
                    chessBoard.Board[x, y] = '.';
                    boardGUI[x, y].Piece = '.';
                }
                
                
            }
            
        }

    }
}

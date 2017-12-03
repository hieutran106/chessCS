using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessCS.View
{
    public class ChessBoardControl: Control 
    {
        public static Color darkColor = Color.FromArgb(118, 150, 86);
        public static Color whiteColor = Color.FromArgb(238, 238, 210);

        public ChessBoard ChessBoard { get; set; }
        public const int SIZE = 60;
        private bool showCoordinate;
        public bool ShowCoordinate
        {
            get
            {
                return showCoordinate;
            }
            set
            {
                showCoordinate = value;
                this.Invalidate();
            }
        }
        private List<Point> highlightedCells;
        SolidBrush whiteBrush = new SolidBrush(whiteColor);
        SolidBrush darkBrush = new SolidBrush(darkColor);
        Font font = new Font("Arial", 10);

        public ChessBoardControl(ChessBoard chessBoard)
        {
            this.Location = new Point(0, 30);
            this.Size = new Size(8 * SIZE, 8 * SIZE);
            highlightedCells = new List<Point>();
            showCoordinate = true;
            this.ChessBoard = chessBoard;
            this.DoubleBuffered = true;
        }
        //
        public void HighlighCell(Point cell)
        {
            highlightedCells.Add(cell);
            //this.Invalidate();
        }
        public void HighlighCells(List<Move> possibleMoves)
        {
            foreach (Move move in possibleMoves)
            {
                Point p = new Point(move.X_Des, move.Y_Des);
                highlightedCells.Add(p);
            }
            this.Invalidate();
        }
        public void RemoveHighlightCells()
        {
            highlightedCells.Clear();
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintTheBoard(e);
            DrawChessPieces(e);
            PaintHighlighSquare(e);        
        }
        private void PaintTheBoard(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(whiteBrush, new Rectangle(0, 0, 8 * SIZE, 8 * SIZE));
            for (int i = 0; i < 64; i++)
            {
                int col = i % 8;
                int row = i / 8;
                if ((col + row) % 2 == 1)
                {
                    g.FillRectangle(darkBrush, new Rectangle(col * SIZE, row * SIZE, SIZE, SIZE));

                }
                PaintCoordinate(e,font,row,col);           
            }

        }
        private void DrawChessPieces(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < 64; i++)
            {
                int col = i % 8;
                int row = i / 8;
                char piece = ChessBoard.Board[row, col];
                if (piece != '.')
                {

                    string dir = Path.GetDirectoryName(Application.ExecutablePath);
                    string filename = Path.Combine(dir, "img\\" + (char.IsUpper(piece) ? "w" : "b") + piece.ToString().ToUpper() + ".png");
                    Image image = Image.FromFile(filename);
                    
                    
                        e.Graphics.DrawImage(image, col * SIZE, row * SIZE, SIZE, SIZE);
                    

                }
            }
            //draw piece animation
            
        }

        private void PaintHighlighSquare(PaintEventArgs e)
        {
            if (highlightedCells.Count!=0)
            {
                Pen selPen = new Pen(Color.PaleVioletRed,3);
                
                foreach (Point point in highlightedCells)
                {
                    int row = point.X;
                    int col = point.Y;
                    e.Graphics.DrawRectangle(selPen, col * SIZE, row * SIZE, SIZE, SIZE);
                }
                selPen.Dispose();
            }
        }
        private void PaintCoordinate(PaintEventArgs e,Font font, int row, int col)
        {
            //Show coordinate
            if (showCoordinate)
            {
                e.Graphics.DrawString($"[{row},{col}]", font, Brushes.Red, 2 + col*SIZE, 2+row*SIZE);  
            }
        }
    }
}

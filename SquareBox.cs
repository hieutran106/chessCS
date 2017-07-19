using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessCS
{
    class SquareBox: PictureBox
    {
        public static Color darkColor = Color.FromArgb(118, 150, 86);
        public static Color whiteColor = Color.FromArgb(238, 238, 210);

        public const int SIZE = 50;
        public int X { set; get; }
        public int Y { set; get; }
        private bool isWhiteSquare;

        private char piece;
        public char Piece {
            get { return piece; } set {
                piece = value;
                if (piece=='.')
                {
                    this.BackgroundImage = null;
                } else
                {
                    string dir = Path.GetDirectoryName(Application.ExecutablePath);
                    string filename = Path.Combine(dir, "img\\" + (char.IsUpper(piece) ? "w" : "b") + piece.ToString().ToUpper() + ".png");
                    this.BackgroundImage = System.Drawing.Image.FromFile(filename);
                }
                
            } }

        public SquareBox(int x, int y) :base()
        {
            X = x;
            Y = y;
            Piece = '.';
            this.Size = new Size(SIZE, SIZE);
            this.Location = new Point(x * SIZE, y * SIZE);
            this.BackColor = ((x + y) % 2 == 0) ? whiteColor : darkColor;

        } 
    }
}

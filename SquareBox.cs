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

        public const int SIZE = 60;
        public int X { set; get; }
        public int Y { set; get; }
        private bool isHighlight;
        public bool IsHighlight { get { return isHighlight; } set {
                isHighlight = value;
                this.Invalidate();
                if (isHighlight)
                {
                    Console.WriteLine($"Hightlight [{X},{Y}]");
                } else
                {
                    Console.WriteLine($"Remove hightlight [{X},{Y}]");
                }
               
            } }

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
        private char piece;
        public char Piece {
            get { return piece; } set {
                piece = value;
                if (piece=='.')
                {
                    this.ImageLocation = null;
                } else
                {
                    string dir = Path.GetDirectoryName(Application.ExecutablePath);
                    string filename = Path.Combine(dir, "img\\" + (char.IsUpper(piece) ? "w" : "b") + piece.ToString().ToUpper() + ".png");
                    this.ImageLocation = filename;
                }
                
            } }

        public SquareBox(int x, int y) :base()
        {
            X = x;
            Y = y;
            showCoordinate = false;
            piece = '.';
            this.Size = new Size(SIZE, SIZE);
            this.Location = new Point(y * SIZE, 30+x * SIZE);
            this.BackColor = ((x + y) % 2 == 0) ? whiteColor : darkColor;
            SizeMode = PictureBoxSizeMode.StretchImage;
            ShowCoordinate = true;
            
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (IsHighlight)
            {
                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Red, ButtonBorderStyle.Solid);
            }
            if (showCoordinate)
            {
                using (Font myFont = new Font("Arial", 10))
                {
                    e.Graphics.DrawString($"[{X},{Y}]", myFont, Brushes.Red, new Point(2, 2));
                }
            }
        }
        
    }
}

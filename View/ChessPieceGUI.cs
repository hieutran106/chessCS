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
    public class ChessPieceGUI: PictureBox
    {
        public const int SIZE = 60;
        public int X { set; get; }
        public int Y { set; get; }

        private char piece;
        public char Piece
        {
            get { return piece; }
            set
            {
                piece = value;
                if (piece == '.')
                {
                    this.ImageLocation = null;
                }
                else
                {
                    string dir = Path.GetDirectoryName(Application.ExecutablePath);
                    string filename = Path.Combine(dir, "img\\" + (char.IsUpper(piece) ? "w" : "b") + piece.ToString().ToUpper() + ".png");
                    this.ImageLocation = filename;
                }
               

            }
        }

        public ChessPieceGUI(int x, int y) : base()
        {
            X = x;
            Y = y;
            
            this.Size = new Size(SIZE, SIZE);
            this.Location = new Point(Y * SIZE,30 + X * SIZE);
            
            this.BackColor = Color.Transparent;
            
            SizeMode = PictureBoxSizeMode.StretchImage;
            
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;

            if (this.Parent != null)
            {
                var index = Parent.Controls.GetChildIndex(this);
                for (var i = Parent.Controls.Count - 1; i > index; i--)
                {
                    var c = Parent.Controls[i];
                    if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
                    {
                        using (var bmp = new Bitmap(c.Width, c.Height, g))
                        {
                            c.DrawToBitmap(bmp, c.ClientRectangle);
                            g.TranslateTransform(c.Left - Left, c.Top - Top);
                            g.DrawImageUnscaled(bmp, Point.Empty);
                            g.TranslateTransform(Left - c.Left, Top - c.Top);
                        }
                    }
                }
            }
        }
    }
}

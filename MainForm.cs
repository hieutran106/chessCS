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
        public const int SIZE = 50;
        public MainForm()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        //Draw chess board
                        Color color = ((i + j) % 2 == 0) ? Color.White : Color.Black;
                        brush.Color = color;
                        g.FillRectangle(brush, i * SIZE, j * SIZE, SIZE, SIZE);
                        //draw chess piece
                    }
            }
        }
    }
}

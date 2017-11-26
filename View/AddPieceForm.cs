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
    public partial class AddPieceForm : Form
    {
        private int x, y;
        private MainForm mainForm;

        private char piece='p';
        private char color='b';

        private void button1_Click(object sender, EventArgs e)
        {
            char newPiece = (color == 'w') ? char.ToUpper(piece) : char.ToLower(piece);
            mainForm.Put_Piece(x, y, newPiece);
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public AddPieceForm(int x, int y, MainForm mainForm)
        {
            InitializeComponent();
            this.x = x;
            this.y = y;
            this.mainForm = mainForm;
        }
        //change piece type based on option chosen by sender
        private void pieceType_CheckedChanged(object sender, EventArgs e)
        {
            if (sender==pawnRBtn)
            {
                piece = 'p';
            } else if (sender==rookRBtn)
            {
                piece = 'r';
            } else if (sender == knightRBtn)
            {
                piece = 'n';
            } else if (sender == bishopRBtn)
            {
                piece = 'b';
            } else if (sender == queenRBtn)
            {
                piece = 'q';
            } else if (sender == kingRBtn)
            {
                piece = 'k';
            }
        }
        //change color based on option chosen by sender
        private void color_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == whiteRBtn)
            {
                color = 'w';
            } else if (sender == blackRBtn)
            {
                color = 'b';
            }

        }
    }
}

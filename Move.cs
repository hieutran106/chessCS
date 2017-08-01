using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS
{
    public class Move
    {
        public int X_Des { get; set; }
        public int Y_Des { get; set; }
        public int X_Src { get; set; }
        public int Y_Src { get; set; }
        public char Piece { get; private set; }
        public char Capture { get; private set; }
        public bool PawnPromotion { get; set; }
        public Move(int x_src, int y_src, int x_des, int y_des, ChessBoard chessBoard)
        {
            X_Src = x_src;
            Y_Src = y_src;
            X_Des = x_des;
            Y_Des = y_des;
            Piece = chessBoard.Board[x_src, y_src];
            Capture = chessBoard.Board[x_des, y_des];
            PawnPromotion = false;
        }

        public static string PositionFromCoordinate(int x, int y)
        {
            
                StringBuilder position = new StringBuilder(2);
                position.Append((char)(x + 97));
                position.Append((char)(56 - y));
                return position.ToString();
        }
        public override string ToString()
        {
            StringBuilder move = new StringBuilder(40);
            move.Append($"{Piece}:[{X_Src},{Y_Src}]-[{X_Des},{Y_Des}]");
            if (Capture=='.')
            {
                move.Append(" --");
            } else
            {
                move.Append($" x{Capture}");
            }
            if (PawnPromotion)
            {
                move.Append("=Q");
            }
            else move.Append("  ");
            return move.ToString();
        }
        public bool Equals(Move other)
        {
            if (X_Src == other.X_Src &&
                Y_Src == other.Y_Src &&
                X_Des == other.X_Des &&
                Y_Des == other.Y_Des &&
                Piece == other.Piece &&
                Capture == other.Capture &&
                PawnPromotion == other.PawnPromotion)
                return true;
            else return false;
        }
    }
}

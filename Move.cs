using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS
{
    public class Move : IComparable<Move>
    {
        public int X_Des { get; set; }
        public int Y_Des { get; set; }
        public int X_Src { get; set; }
        public int Y_Src { get; set; }
        public int Src { get; set; }
        public int Dst { get; set; }

        public char Piece { get; private set; }
        public char Capture { get; private set; }
        public bool PawnPromotion { get; set; }
        //use for move ordering
        public int Value { get; set; }
        public Move(int x_src, int y_src, int x_dst, int y_dst, ChessBoard chessBoard)
        {
            X_Src = x_src;
            Y_Src = y_src;
            X_Des = x_dst;
            Y_Des = y_dst;
            //
            Src = x_src * 8 + y_src;
            Dst = x_dst * 8 + y_dst;
            //
            Piece = chessBoard.Board[x_src, y_src];
            Capture = chessBoard.Board[x_dst, y_dst];
            if (chessBoard.Board[x_src, y_src] == 'P')
            {
                if (x_dst == 0)
                {
                    PawnPromotion = true;
                }
            }
            else if (chessBoard.Board[x_src, y_src] == 'p')
            {
                if (x_dst == 7)
                {
                    PawnPromotion = true;
                }
            }
        }
        public void Evaluate()
        {
            int score = 0;
            switch (char.ToUpper(Piece))
            {
                case 'P':
                    score = 1;
                    break;
                case 'N':
                    score = 3;
                    break;
                case 'B':
                    score = 3;
                    break;
                case 'R':
                    score = 5;
                    break;
                case 'Q':
                    score = 9;
                    break;
                case 'K':
                    score = 90;
                    break;
            }
            if (char.IsUpper(Piece))
            {
                Value = score;
            } else
            {
                Value = score;
            }
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
            if (Capture == '.')
            {
                move.Append(" --");
            }
            else
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

        public int CompareTo(Move other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}

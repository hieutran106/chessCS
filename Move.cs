using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS
{
    public class Move
    {
        public int Des_X { get; set; }
        public int Des_Y { get; set; }
        public int Src_X { get; set; }
        public int Src_Y { get; set; }
        public bool IsCapture { get; private set; }
        public bool PawnPromotion { get; set; }
        public Move(int src_x, int src_y, int des_x, int des_y, ChessBoard chessBoard)
        {
            Src_X = src_x;
            Src_Y = src_y;
            Des_X = des_x;
            Des_Y = des_y;
            if (chessBoard.Board[des_x, des_y] == '.')
            {
                IsCapture = true;
            }
            else IsCapture = false;
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
            move.Append(PositionFromCoordinate(Src_X, Src_Y));
            if (IsCapture)
            {
                move.Append(" -x");
            }
            else move.Append(" --");
            move.Append(PositionFromCoordinate(Des_X, Des_Y));
            if (PawnPromotion)
            {
                move.Append("=Q");
            }
            move.Append($" ([{Src_X},{Src_Y}] - [{Des_X},{Des_Y}])");
            return move.ToString();
        }
    }
}

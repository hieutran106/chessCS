using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS.ChessPieces
{
    class Knight
    {
        static int[,] delta = { { -1, -2 }, { -2, -1 }, { -2, +1 }, { -1, +2 }, { 1, +2 }, { 2, 1 }, { +2, -1 }, { 1, -2 } };
        public static List<string> generateMove(int x, int y, ChessBoard chessBoard)
        {
            List<string> moves = new List<string>();
            for (int i=0;i<delta.GetLength(0);i++)
            {
                int x_des = x + delta[i,0];
                int y_des = y + delta[i,1];
                if (ChessBoard.IsValidCoordinate(x_des,y_des) && (chessBoard.Board[x_des, y_des] == '.' || chessBoard.HasWhitePiece(x_des, y_des))) {
                    string move = chessBoard.GetMove(x, y, x, y + 1);
                    moves.Add(move);
                }
            }
            return moves;
        }
    }
}

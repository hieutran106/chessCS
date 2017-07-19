using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS.ChessPieces
{
    class Bishop
    {
        public static List<Move> generateMove(int x, int y, ChessBoard chessBoard)
        {
            List<Move> moves = new List<Move>();
            for (int i = -1; i <= 1; i=i+2)
                for (int j = -1; j <= 1; j=j+2)
                {
                    //only move in diagonal lines
                    int step = 1;
                    int des_x = x + i * step;
                    int des_y = y + y * step;
                    while (ChessBoard.IsValidCoordinate(des_x, des_y) && chessBoard.CanBlackMove(des_x, des_y))
                    {
                        Move move = new Move(x, y, des_x, des_y, chessBoard);
                        moves.Add(move);
                        step++;
                    }
                }
            return moves;
        }
    }
}

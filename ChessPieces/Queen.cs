using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS.ChessPieces
{
    class Queen
    {
        public static List<Move> generateMove(int x, int y, ChessBoard chessBoard)
        {
            List<Move> moves = new List<Move>();
            //Color of chess piece at [x,y]
            char color = char.IsUpper(chessBoard.Board[x, y]) ? 'w' : 'b';
            for (int i=-1;i<=1;i++) 
                for (int j=-1;j<=1;j++)
                {
                    int step = 1;
                    int des_x = x + i * step;
                    int des_y = y + y * step;
                    while (ChessBoard.IsValidCoordinate(des_x,des_y) && chessBoard.CanMakeMove(des_x,des_y,color))
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

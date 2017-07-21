using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS.ChessPieces
{
    class Pawn :ChessPiece
    {
        public static List<Move> generateMove(int x, int y, ChessBoard chessBoard)
        {
            List<Move> moves = new List<Move>();
            //Color of chess piece at [x,y]
            bool color = char.IsUpper(chessBoard.Board[x, y]);
            int dy = (color == ChessPiece.BLACK) ? 1 : -1;

             
            //Move ahead
            if (ChessBoard.IsValidCoordinate(x, y + dy) && chessBoard.Board[x, y + dy] == '.')
            {
                Move move = chessBoard.GetMove(x, y, x, y + 1);
                //promotion
                if ((color==BLACK && y==6)|| (!color==WHITE && y==1)) {
                    move.PawnPromotion = true;
                }
                moves.Add(move);
            }
            
            
            //capture, diagonally forward one square to the left or right
            //dx = -1 : to the left; dx = 1: to the right
            for (int dx=-1;dx<=1;dx++)
            {
                int x_des = x + dx;
                int y_des = y + dy;
                if (ChessBoard.IsValidCoordinate(x_des,y_des) && chessBoard.CanCapture(x_des,y_des,color))
                {
                    Move move = chessBoard.GetMove(x, y, x_des, y_des);
                    //promotion
                    if ((color==BLACK && y == 6) || (color==WHITE && y == 1))
                    {
                        move.PawnPromotion = true;
                    }
                    moves.Add(move);
                }
            }
            //Move two squares from starting point
            if (color==WHITE)
            {
                if (y==6 && chessBoard.Board[x,5]=='.' && chessBoard.Board[x,4]=='.')
                {
                    Move move = chessBoard.GetMove(x, y, x, 4 );
                    moves.Add(move);
                }
            } else //color = BLACK
            {
                if (y == 1 && chessBoard.Board[x, 2] == '.' && chessBoard.Board[x, 3] == '.')
                {
                    Move move = chessBoard.GetMove(x, y, x, 3);
                    moves.Add(move);
                }
            }
            return moves;
            
        }
    }
}

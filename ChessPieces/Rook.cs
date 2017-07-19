using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS.ChessPieces
{
    class Rook
    {
        public static List<Move> generateMove(int x, int y, ChessBoard chessBoard)
        {
            List<Move> moves = new List<Move>();
            //vertical moves
            if (x>0)
            {
                for (int i = x-1; i >= 0; i--)
                {
                    if (chessBoard.Board[i, y] == '.')
                    {
                        Move move = chessBoard.GetMove(x, y, i, y);
                        moves.Add(move);
                    }
                    else break;
                }

            }
             
            if (x<7)
            {
                for (int i=x+1;i<=7;i++)
                {
                    if (chessBoard.Board[i, y] == '.')
                    {
                        Move move = chessBoard.GetMove(x, y, i, y);
                        moves.Add(move);
                    }
                    else break;
                }
            }
            //horizontal moves
            if (y>0)
            {
                for (int i=y-1;i>=0;i--)
                {
                    if (chessBoard.Board[x, i] == '.')
                    {
                        Move move = chessBoard.GetMove(x, y, x, i);
                        moves.Add(move);
                    }
                    else break;
                }
            }
            if (y<7)
            {
                for (int i=y+1;y<=7;i++)
                {
                    if (chessBoard.Board[x,i]=='.')
                    {
                        Move move = chessBoard.GetMove(x, y, x, i);
                        moves.Add(move);
                    }
                }
            }
            return null;
        }
    }
}

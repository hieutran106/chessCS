using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS.ChessPieces
{
    class Pawn
    {
        public static List<string> generateMove(int x, int y, ChessBoard chessBoard)
        {
            List<string> moves = new List<string>();
            if (chessBoard.Board[x,y]=='p') //Black pawn
            {
                if (ChessBoard.IsValidCoordinate(x,y+1) && chessBoard.Board[x,y+1]=='.')
                {
                    string move=chessBoard.GetMove(x, y, x, y + 1);
                    moves.Add(move);
                }
                if (ChessBoard.IsValidCoordinate(x+1,y+1) && chessBoard.Board[x+1,y+1]!='.' && chessBoard.HasWhitePiece(x+1,y+1))
                {
                    string move = chessBoard.GetMove(x, y, x+1, y + 1);
                    moves.Add(move);
                }
                if (ChessBoard.IsValidCoordinate(x - 1, y - 1) && chessBoard.Board[x + 1, y - 1] != '.' && chessBoard.HasWhitePiece(x + 1, y - 1))
                {
                    string move = chessBoard.GetMove(x, y, x - 1, y -1);
                    moves.Add(move);
                }
                if (x==1 && chessBoard.Board[x,y+1]!='.' && chessBoard.Board[x,y+2]=='.')
                {
                    string move = chessBoard.GetMove(x, y, x, y + 2);
                    moves.Add(move);
                }
                return moves;
            } else return null;
        }
    }
}

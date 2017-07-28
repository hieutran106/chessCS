using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS
{
    class ChessAI
    {
        public static Move getMove(ChessBoard board)
        {
            if (board.Fullmove==1 && board.ActiveColor==ChessBoard.BLACK)
            {
                Move move = board.GetMove(1,4,3,4);
                return move;
            } else if (board.Fullmove==2 && board.ActiveColor==ChessBoard.BLACK)
            {
                Move move = board.GetMove(0,1,2,2);
                return move;
            } else
            {
                return null;
            }
        }
    }
}

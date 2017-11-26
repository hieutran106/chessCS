using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS
{
    public class Search
    {
        public static bool BLACK = false;
        public static bool WHITE = true;


        static int globalDepth = 4;
        private Evaluation evaluation;
        private ChessBoard examinedBoard;
        public Search()
        {
            evaluation = new Evaluation();
        }

        public static Move SearchMove(ChessBoard examinedBoard, bool player)
        {
            Search search = new Search();
            search.examinedBoard = examinedBoard.FastCopy();
            MNResult result = search.AlphaBeta(4, 1000000, -1000000, null, player);
            return result.Move;

        }
        private MNResult AlphaBeta(int depth, int beta, int alpha, Move move, bool player)
        {
            //BLACK is max player
            bool isMaxPlayer = (player == BLACK) ? true : false;

            if (depth == 0)
            {
                return EvaluateNode(move, player);
            }

            List<Move> possibleMoves = examinedBoard.PossibleMoves(player);
            if (possibleMoves.Count == 0)
            {
                return EvaluateNode(move, player);
            }
            //sort later           
            foreach (Move eleMove in possibleMoves)
            {

                examinedBoard.MakeMove(eleMove);
                bool nextPlayer = !player;
                MNResult result = AlphaBeta(depth - 1, beta, alpha, eleMove, nextPlayer);
                int value = result.Value;

                examinedBoard.UndoMove(eleMove);
                //BLACK is Max Player
                if (isMaxPlayer)
                {
                    if (value < beta) //Max Nodes can only make restriction on the lower bound
                    {
                        beta = value;
                        if (depth == globalDepth)
                        {
                            move = result.Move;
                        }
                    }
                }
                else
                {
                    if (value > alpha)
                    {
                        alpha = value;
                        if (depth == globalDepth)
                        {
                            move = result.Move;
                        }
                    }
                }
                if (alpha >= beta) //pruning
                {
                    if (isMaxPlayer)
                    {
                        return new MNResult(move, alpha);
                    }
                    else
                    {
                        return new MNResult(move, beta);
                    }
                }

            }

            // Travel all child node, no prunning
            if (isMaxPlayer)
            {
                //value of node is alpha value
                return new MNResult(move, alpha);
            }
            else
            {
                //value of min node is beta value
                return new MNResult(move, beta);
            }
        }
        private MNResult EvaluateNode(Move move, bool player)
        {
            
            int score = evaluation.EvaluateBoard(this.examinedBoard);
            //Negate the value
            if (player == BLACK)
                score = -score;
            MNResult result = new MNResult(move, score);
            return result;
        }
    }
}

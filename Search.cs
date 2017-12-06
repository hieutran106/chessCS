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
        private int visitedNode = 0;

        private int maxDepth;
        private Evaluation evaluation;
        private ChessBoard examinedBoard;
        private Move[,] searchKiller;
        private bool nullPruning;
        public Search(int maxDepth)
        {
            evaluation = new Evaluation();
            this.maxDepth = maxDepth;
            searchKiller = new Move[maxDepth, 2];
            this.nullPruning = true;
        }

        public static Move SearchMove(ChessBoard examinedBoard, bool player, bool debug)
        {
            Search search = new Search(4);
            search.examinedBoard = examinedBoard.FastCopy();
            int color = (player == BLACK) ? 1 : -1;
            MNResult bestResult = search.RootNegaMax(search.maxDepth, -10000, 10000, color, false);
            Console.WriteLine("Best value:" + bestResult.Value + " Visited node:" + search.visitedNode);
            return bestResult.Move;

        }
        private MNResult RootNegaMax(int depth, int alpha, int beta, int color, bool debug)
        {
            List<Move> possibleMoves = null;
            possibleMoves = (debug) ? DebugLegalMoves() : examinedBoard.LegalMovesForPlayer(color);

            int bestValue = Int32.MinValue;
            Move bestMove = null;
            //sort move
            OrderMove(possibleMoves, depth);
            //
            foreach (Move eleMove in possibleMoves)
            {
                visitedNode++;
                examinedBoard.MakeMove(eleMove);
                //Print information
                Console.WriteLine(new string('\t', maxDepth - depth) + eleMove.ToString());
                int value = -Negamax(depth - 1, -beta, -alpha, -color, debug,nullPruning);
                examinedBoard.UndoMove(eleMove);

                if (value > bestValue)
                {
                    bestValue = value;
                    bestMove = eleMove;
                }

                alpha = Math.Max(alpha, value);
                if (alpha >= beta)
                {
                    Console.WriteLine(new string('\t', maxDepth - depth) + "Cut off");
                    break;
                }

            }
            return new MNResult(bestMove, bestValue);
        }
        private int Negamax(int depth, int alpha, int beta, int color, bool debug, bool allowNull)
        {
            if (depth <= 0)
            {
                int score = Evaluate(color, debug, depth);
                return score;
            }
            //Null move
            if (allowNull)
            {
                int nullTurn = -color;
                int val = -Negamax(depth - 1 - 2, -beta, -beta + 1, nullTurn, debug, false);
                if (val >= beta)
                {
                    return val; //Cut off
                }
                    
            }

            List<Move> possibleMoves = null;
            possibleMoves = (debug) ? DebugLegalMoves() : examinedBoard.LegalMovesForPlayer(color);
            if (possibleMoves.Count == 0)
            {
                int score = Evaluate(color, debug, depth);
                return score;
            }
            int bestValue = Int32.MinValue;
            OrderMove(possibleMoves, depth);
            foreach (Move eleMove in possibleMoves)
            {
                visitedNode++;
                examinedBoard.MakeMove(eleMove);
                //Print information
                Console.Write("\n" + new string('\t', maxDepth - depth) + eleMove.ToString());
                int value = -Negamax(depth - 1, -beta, -alpha, -color, debug,nullPruning);
                examinedBoard.UndoMove(eleMove);
                bestValue = Math.Max(alpha, value);
                alpha = Math.Max(alpha, value);
                if (alpha >= beta)
                {
                    Console.Write(new string('\t', maxDepth - depth) + "Cut off");
                    //Save killer move
                    if (eleMove.Capture == '.')
                    {
                        searchKiller[maxDepth - depth, 1] = searchKiller[maxDepth - depth, 0];
                        searchKiller[maxDepth - depth, 0] = eleMove;
                    }
                    break;
                }

            }
            return bestValue;
        }
        private List<Move> DebugLegalMoves()
        {
            Console.Write("How many moves are there:");
            int count = Convert.ToInt32(Console.ReadLine());
            List<Move> possibleMoves = new List<Move>();
            for (int i = 0; i < count; i++)
            {
                Move debugMove = new Move(1, 4, 3, 4, this.examinedBoard);
                possibleMoves.Add(debugMove);
            }
            return possibleMoves;
        }
        private int Evaluate(int color, bool debug, int depthleft)
        {
            int score;
            if (!debug)
            {
                score = evaluation.EvaluateBoard(this.examinedBoard) + (maxDepth - depthleft) * 10;
                score = -color * score;

                Console.WriteLine(" Eva:" + score);
            }
            else
            {
                Console.Write("What is the score:");
                score = Convert.ToInt32(Console.ReadLine());
            }

            return score;
        }
        private void OrderMove(List<Move> possibleMoves, int depth)
        {
            bool killerHeuristic = true;
            foreach (Move eleMove in possibleMoves)
            {
                eleMove.Evaluate();
                if (depth == maxDepth || !killerHeuristic)
                    continue;

                KillerHeuristic(eleMove, depth);
            }
            //sort move
            possibleMoves.Sort();
        }

        private void KillerHeuristic(Move eleMove, int depth)
        {
            //killer heuristic
            Move primaryKiller = searchKiller[maxDepth - depth, 0];
            Move secondaryKiller = searchKiller[maxDepth - depth, 1];
            if (primaryKiller != null)
            {
                if (eleMove.Equals(primaryKiller))
                {
                    eleMove.Value += 90;
                }
            }
            else if (secondaryKiller != null)
            {
                if (eleMove.Equals(secondaryKiller))
                {
                    eleMove.Value += 80;
                }
            }
        }



        private MNResult AlphaBeta(int depth, int beta, int alpha, Move move, bool player, bool debug = false)
        {
            //BLACK is max player
            bool isMaxPlayer = (player == BLACK) ? true : false;

            if (depth == 0)
            {
                return EvaluateNode(move, player, debug);
            }
            List<Move> possibleMoves = null;
            if (!debug)
            {
                possibleMoves = examinedBoard.PossibleMoves(player);
            }
            else
            {
                Console.Write("How many moves are there:");
                int count = Convert.ToInt32(Console.ReadLine());
                possibleMoves = new List<Move>();


                for (int i = 0; i < count; i++)
                {
                    Move debugMove = new Move(1, 4, 3, 4, this.examinedBoard);
                    possibleMoves.Add(debugMove);
                }

            }


            if (possibleMoves.Count == 0)
            {
                return EvaluateNode(move, player, debug);
            }
            //sort later           
            foreach (Move eleMove in possibleMoves)
            {

                examinedBoard.MakeMove(eleMove);
                bool nextPlayer = !player;
                MNResult result = AlphaBeta(depth - 1, beta, alpha, eleMove, nextPlayer, debug);
                int value = result.Value;

                examinedBoard.UndoMove(eleMove);
                //BLACK is Max Player
                if (isMaxPlayer)
                {
                    if (value > alpha) //Max Nodes can only make restriction on the lower bound
                    {
                        alpha = value;
                        if (depth == maxDepth)
                        {
                            move = result.Move;
                        }
                    }

                }
                else
                {
                    if (value < beta)
                    {
                        beta = value;
                        if (depth == maxDepth)
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
        private MNResult EvaluateNode(Move move, bool player, bool debug)
        {
            int score;
            if (!debug)
            {
                score = evaluation.EvaluateBoard(this.examinedBoard);
                //Negate the value
                if (player == BLACK)
                    score = -score;

            }
            else
            {
                Console.Write("What is the score:");
                score = Convert.ToInt32(Console.ReadLine());
            }
            MNResult result = new MNResult(move, score);
            return result;

        }
    }
}

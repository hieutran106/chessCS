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

        public static Move SearchMove(ChessBoard examinedBoard, bool player,bool debug)
        {
            Search search = new Search();
            search.examinedBoard = examinedBoard.FastCopy();
            int color = (player == BLACK) ? 1 : -1;
            MNResult bestResult = search.RootNegaMax(4, -10000, 10000, color, false);
            Console.WriteLine("Best value:" + bestResult.Value);
            return bestResult.Move;

        }
        private MNResult RootNegaMax(int depth, int alpha, int beta, int color, bool debug)
        {         
            List<Move> possibleMoves = null;
            possibleMoves = (debug) ? DebugLegalMoves() : examinedBoard.LegalMovesForPlayer(color);
  
            int bestValue = Int32.MinValue;
            Move bestMove = null;
            foreach (Move eleMove in possibleMoves)
            {
                examinedBoard.MakeMove(eleMove);
                //Print information
                Console.WriteLine(new string('\t', globalDepth - depth) + eleMove.ToString());
                int value = -Negamax(depth - 1, -beta, -alpha, -color, debug);               
                examinedBoard.UndoMove(eleMove);

                if (value>bestValue)
                {
                    bestValue = value;
                    bestMove = eleMove;
                }
                
                alpha = Math.Max(alpha, value);
                if (alpha >= beta)
                {
                    Console.WriteLine(new string('\t', globalDepth - depth) + "Cut off");
                    break;
                }
                    
            }
            return new MNResult(bestMove, bestValue);
        }
        private int Negamax(int depth, int alpha, int beta, int color, bool debug)
        {
            if (depth == 0)
            {
                int score= Evaluate(color, debug,depth);
                return score;
            }
            List<Move> possibleMoves = null;
            possibleMoves = (debug)?DebugLegalMoves(): examinedBoard.LegalMovesForPlayer(color);
            if (possibleMoves.Count==0)
            {
                int score = Evaluate(color, debug,depth);
                return score;
            }
            int bestValue = Int32.MinValue;
            foreach (Move eleMove in possibleMoves) {
                examinedBoard.MakeMove(eleMove);
                //Print information
                Console.Write("\n"+new string('\t', globalDepth - depth) + eleMove.ToString());
                //if  (depth!=1)
                //{
                //    Console.WriteLine();
                //}
                int value = -Negamax(depth - 1, -beta, -alpha, -color,debug);     
                examinedBoard.UndoMove(eleMove);
                bestValue = Math.Max(alpha, value);
                alpha = Math.Max(alpha, value);
                if (alpha >= beta)
                {
                    Console.Write(new string('\t', globalDepth - depth) + "Cut off");
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
                score = evaluation.EvaluateBoard(this.examinedBoard) + (globalDepth - depthleft) * 10;
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






        private MNResult AlphaBeta(int depth, int beta, int alpha, Move move, bool player,bool debug=false)
        {
            //BLACK is max player
            bool isMaxPlayer = (player == BLACK) ? true : false;

            if (depth == 0)
            {
                return EvaluateNode(move, player,debug);
            }
            List<Move> possibleMoves=null;
            if (!debug)
            {
                possibleMoves = examinedBoard.PossibleMoves(player);
            } else
            {
                Console.Write("How many moves are there:");
                int count = Convert.ToInt32(Console.ReadLine());
                possibleMoves = new List<Move>();


                for (int i=0;i<count;i++)
                {
                    Move debugMove = new Move(1, 4, 3, 4,this.examinedBoard);
                    possibleMoves.Add(debugMove);
                }

            }

            
            if (possibleMoves.Count == 0)
            {
                return EvaluateNode(move, player,debug);
            }
            //sort later           
            foreach (Move eleMove in possibleMoves)
            {

                examinedBoard.MakeMove(eleMove);
                bool nextPlayer = !player;
                MNResult result = AlphaBeta(depth - 1, beta, alpha, eleMove, nextPlayer,debug);
                int value = result.Value;

                examinedBoard.UndoMove(eleMove);
                //BLACK is Max Player
                if (isMaxPlayer)
                {
                    if (value > alpha) //Max Nodes can only make restriction on the lower bound
                    {
                        alpha = value;
                        if (depth == globalDepth)
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
        private MNResult EvaluateNode(Move move, bool player, bool debug)
        {
            int score;
            if (!debug)
            {
                score = evaluation.EvaluateBoard(this.examinedBoard);
                //Negate the value
                if (player == BLACK)
                    score = -score;
               
            } else
            {
                Console.Write("What is the score:");
                score = Convert.ToInt32(Console.ReadLine());
            }
            MNResult result = new MNResult(move, score);
            return result;

        }
    }
}

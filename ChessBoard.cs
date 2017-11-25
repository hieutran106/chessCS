using ChessCS.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS
{
    public class ChessBoard
    {
        /*
         * chess piece = WHITE / black
         * pawn = P/p
         * Rook = R/r
         * Knight = N/n
         * Bishop = B/b
         * Queen = Q/q
         * King = K/k
         *  
         */
        public static bool BLACK = false;
        public static bool WHITE = true;
        //check, checkmate and stalemate

        public bool BlackCheck { get; private set; }
        public bool BlackCheckMate { get; private set; }
        public bool WhiteCheck { get; private set; }
        public bool WhiteCheckMate { get; private set; }
        public bool StaleMate { get; private set; }


        private bool activeColor;
        public bool ActiveColor
        {
            get {
                return activeColor;
            }
        }
        private int fullMove;
        public int Fullmove
        {
            get
            {
                return fullMove;
            }
        }
        public char[,] Board { get; set; }
        static int globalDepth = 4;
        private Rating rating;
        public ChessBoard()
        {
            Board = new char[8, 8];
            rating = new Rating();
        }
        
        //Reset the board
        public void Reset()
        {
            //FEN for starting position
            string startingPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w";
            Load(startingPosition);
            activeColor = WHITE;
            fullMove = 1;
        }
        //Returns the FEN string for the current board
        public string GetFEN()
        {
            StringBuilder fen_str = new StringBuilder(50);
            for (int i=0;i<8;i++)
            {
                int number = 0;
                for (int j=0;j<8;j++)
                {
                    if (Board[i,j]!='.')
                    {
                        fen_str.Append(Board[i, j]);
                       
                    } else
                    {
                        number++;
                        int next = j + 1;
                        if (next == 8 || (Board[i, next] != '.'))
                        {
                            fen_str.Append(number);
                            number = 0;
                        }
                        

                    }
                }
                if (i<7)
                    fen_str.Append('/');                                 
            }
            return fen_str.ToString();
        }
        public void Load(string fen)
        {
            string[] block = fen.Split(' ');

            //process piece placement
            string piecePlacement = block[0];
            Console.WriteLine(piecePlacement);
            string[] tokens = piecePlacement.Split('/');
            if (tokens.Length==8)
            {
                for (int i=0;i<8;i++)
                {
                    int index = 0;
                    for (int j=0;j<tokens[i].Length;j++)
                    {
                        char charAt = tokens[i][j];
                        int number;
                        if (!int.TryParse(charAt.ToString(), out number))
                        {
                            //Character is a chess piece
                            Board[i, index] = charAt;
                            index++;
                        } else
                        {
                            //insert empty square into board
                            for (int k=0;k<number;k++)
                            {
                                Board[i, index + k] = '.';
                            }
                            index += number;
                        }
                    }
                    if (index != 8)
                        Console.WriteLine("Invalid FEN notation");
                }
            } else
            {
                Console.WriteLine("Invalid FEN notation, reset the board");
            }
            //active color and full move
            string activeColor_str = block[1];
            activeColor = (activeColor_str[0] == 'w') ? WHITE : BLACK;
            //full move
            int.TryParse(block[block.Length-1], out fullMove);
        }
        //Put a piece 
        public void Put(char p,string position)
        {
            if (97<=position[0] && position[0]<=104)
            {
                if (49<=position[1] && position[1]<=56)
                {
                    //convert char 'a'->'h' to number
                    int i = position[0] - 97;
                    //convert char '1' -> '8' to number
                    int j = position[1] - 49;
                    Board[i, j] = p;
                }
            }
        }
        public static bool IsValidCoordinate(int x, int y)
        {
            if ((0 <= x && x <= 7) && (0 <= y && y <= 7))
                return true;
            else return false;
        }
        public Move GetMove(int src_x, int src_y, int des_x, int des_y)
        {
            Move move = new ChessCS.Move(src_x, src_y, des_x, des_y, this);
            return move;
        }
        public void MakeMove(Move move)
        {
            //Update board data
            Board[move.X_Src, move.Y_Src] = '.';
            Board[move.X_Des, move.Y_Des] = move.Piece;
            if (move.PawnPromotion)
            {
                if (activeColor == WHITE)
                    Board[move.X_Des, move.Y_Des] = 'Q';
                else Board[move.X_Des, move.Y_Des] = 'q';
            }
            //update active color and full move
            if (activeColor==WHITE)
            {
                activeColor = BLACK;
            } else
            {
                fullMove++;
                activeColor = WHITE;
            }
        }
        public void UndoMove(Move move)
        {
            //Update board data
            Board[move.X_Src, move.Y_Src] = move.Piece;
            Board[move.X_Des, move.Y_Des] = move.Capture;
            //update active color and full move
            if (activeColor==BLACK)
            {
                activeColor = WHITE;
            } else
            {
                activeColor = BLACK;
                fullMove--;
            }
        }
        public bool CanCapture(int x,int y, bool color)
        {
            if ((color == WHITE && char.IsLower(Board[x, y])) //IsLower --> black
                || (color == BLACK && char.IsUpper(Board[x, y]))) //IsUpper --> white
                return true;
            else return false;
        }
       
        //All possible moves
        public List<Move> PossibleMoves(bool color)
        {
            List<Move> possibleMoves = new List<Move>();
            
            for (int i=0;i<8;i++)
                for (int j=0;j<8;j++)
                {
                    List<Move> pieceMoves = PieceMoves(Board[i, j], color, i, j);
                    if (pieceMoves != null)
                    {
                        //pieceMove is not generated if piece is '.'
                        possibleMoves.AddRange(pieceMoves);
                    }
                }
            
            return possibleMoves;
        }
        private List<Move> PieceMoves(char piece, bool color,int i, int j)
        {
            List<Move> pieceMoves = null;
            if ((color==WHITE&&char.IsUpper(piece)) ||
                (color==BLACK && char.IsLower(piece))) {
                switch (piece)
                {
                    case 'P':
                    case 'p':
                        pieceMoves = Pawn.generateMove(i, j, this);
                        Console.WriteLine($"Pawn move: {pieceMoves.Count}");
                        break;
                    case 'R':
                    case 'r':
                        pieceMoves = Rook.generateMove(i, j, this);
                        Console.WriteLine($"Rook move: {pieceMoves.Count}");
                        break;
                    case 'N':
                    case 'n':
                        pieceMoves = Knight.generateMove(i, j, this);
                        Console.WriteLine($"Knight move: {pieceMoves.Count}");
                        break;
                    case 'B':
                    case 'b':
                        pieceMoves = Bishop.generateMove(i, j, this);
                        Console.WriteLine($"Bishop move: {pieceMoves.Count}");
                        break;
                    case 'Q':
                    case 'q':
                        pieceMoves = Queen.generateMove(i, j, this);
                        Console.WriteLine($"Queen move: {pieceMoves.Count}");
                        break;
                    case 'K':
                    case 'k':
                        pieceMoves = King.generateMove(i, j, this);
                        Console.WriteLine($"King move: {pieceMoves.Count}");
                        break;
                    default:
                        break;
                }
            }
            return pieceMoves;
            
        }
        public void PrintBoard()
        {
            Console.WriteLine("   +------------------------+");
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{8-i} |");
                for (int j=0;j<8;j++)
                {
                    Console.Write($"  {(char)Board[i, j]}");
                }
                Console.WriteLine(" |");
            }
            Console.WriteLine("   +------------------------+");
            Console.WriteLine("     a  b  c  d  e  f  g  h");
        }

        private MNResult EvaluateNode(Move move, bool player)
        {
            
            int evaluation = rating.EvaluateBoard(this);
            //Negate the value
            if (player == BLACK)
                evaluation = -evaluation;
            MNResult result = new MNResult(move, evaluation);
            return result;
        }
        public MNResult AlphaBeta(int depth, int beta, int alpha, Move move, bool player)
        {
            //BLACK is max player
            bool isMaxPlayer = (player == BLACK) ? true : false;
         
            if (depth==0)
            {
                return EvaluateNode(move, player);
            }

            List<Move> possibleMoves = PossibleMoves(player);
            if (possibleMoves.Count==0)
            {
                return EvaluateNode(move, player);
            }
            //sort later           
            foreach (Move eleMove in possibleMoves)
            {
                MakeMove(eleMove);
                bool nextPlayer = !player;
                MNResult result = AlphaBeta(depth - 1, beta, alpha, eleMove, nextPlayer);
                int value = result.Value;
                
                UndoMove(eleMove);
                //BLACK is Max Player
                if (isMaxPlayer)
                {
                    if (value<beta) //Max Nodes can only make restriction on the lower bound
                    {
                        beta = value;
                        if (depth==globalDepth)
                        {
                            move = result.Move;
                        }
                    }
                } else
                {
                    if (value>alpha)
                    {
                        alpha = value;
                        if (depth==globalDepth)
                        {
                            move = result.Move;
                        }
                    }
                }
                if (alpha>=beta) //pruning
                {                   
                    if (isMaxPlayer)
                    {
                        return new MNResult(move, alpha);
                    } else
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
            else {
                //value of min node is beta value
                return new MNResult(move, beta);
            } 

        }
        public Move GetAIMove()
        {
            if (Fullmove == 1 && ActiveColor == ChessBoard.BLACK)
            {
                Move move = GetMove(1, 4, 3, 4);
                return move;
            }
            else if (Fullmove == 2 && ActiveColor == ChessBoard.BLACK)
            {
                Move move = GetMove(0, 1, 2, 2);
                return move;
            }
            else
            {
                bool maxPlayer = BLACK;
                MNResult result= AlphaBeta(4, 1000000, -1000000, null, maxPlayer);
                Console.WriteLine("Best move:" + result.Move);
                return result.Move;
            }
        }
    }
}

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
        public ChessBoard()
        {
            Board = new char[8, 8];
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
       
       
        public List<Move> PossibleMoves()
        {
            List<Move> possibleMoves = new List<Move>();
            for (int i=0;i<8;i++)
                for (int j=0;j<8;j++)
                {
                    List<Move> pieceMoves=null;
                    char piece = char.ToUpper(Board[i, j]);
                    switch (piece)
                    {
                        case 'P':
                            pieceMoves = Pawn.generateMove(i, j, this);
                            break;
                        case 'R':
                            pieceMoves = Rook.generateMove(i, j, this);
                            break;
                        case 'N':
                            pieceMoves = Knight.generateMove(i, j, this);
                            break;
                        case 'B':
                            pieceMoves = Bishop.generateMove(i, j, this);
                            break;
                        case 'Q':
                            pieceMoves = Queen.generateMove(i, j, this);
                            break;
                        case 'K':
                            pieceMoves = King.generateMove(i, j, this);
                            break;
                        default:
                            break;
                    }
                    possibleMoves.AddRange(pieceMoves);                   
                }
            return possibleMoves;
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
        public MNResult alphaBeta(int depth, int beta, int alpha, Move move, bool player)
        {
            //BLACK is max player

            //List<Move> possibleMoves = PossibleMoves();
            List<Move> possibleMoves = new List<Move>();
            Move forTestMove = new Move(1, 4, 3, 4, this);
            possibleMoves.Add(forTestMove);

            if (depth==0 || possibleMoves.Count==0)
            {
                //Negate the value
                int sign = (player == BLACK) ? 1 : -1;
                //MNResult result = new MNResult(move, Rating() * sign);
                MNResult result = new MNResult(move, Rating());
                return result;
            }
            //Prompt how many moves
            possibleMoves.Remove(forTestMove);
            Console.Write("How many moves are there: ");
            int temp= Convert.ToInt32(Console.ReadLine());
            for (int i=0;i< temp;i++)
            {
                Move testMove = new Move(1, 4, 3, 4, this);
                possibleMoves.Add(testMove);
            }
            //sort later
            player = !player;
            foreach (Move eleMove in possibleMoves)
            {
                MakeMove(eleMove);
                MNResult result = alphaBeta(depth - 1, beta, alpha, eleMove, player);
                int value = result.Value;
                UndoMove(eleMove);
                //BLACK is Max Player
                if (player==BLACK)
                {
                    if (value<=beta)
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
                if (alpha>=beta)
                {
                    if (player == BLACK)
                    {
                        return new MNResult(move, beta);
                    }
                    else return new MNResult(move, alpha);
                }
                
            }
            if (player == BLACK)
            {
                return new MNResult(move, beta);
            }
            else return new MNResult(move, alpha);

        }
        public int Rating()
        {
            Console.Write("What is the score: ");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}

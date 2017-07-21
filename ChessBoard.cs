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

        public char[,] Board { get; set; }
        public ChessBoard()
        {
            Board = new char[8, 8];
        }
        
        //Reset the board
        public void Reset()
        {
            //FEN for starting position
            string startingPosition = "rnbqkbnr/pppppppp/3p4/8/8/8/PPPPPPPP/RNBQKBNR w";
            //string startingPosition = "3p4/2K1pp2/3p4/8/8/8/PPPPPPPP/RNBQKBNR w";
            Load(startingPosition);
        }
        //Returns the FEN string for the current board
        public string GetFEN()
        {
            return null;
        }
        public void Load(string fen)
        {
            string piecePlacement = fen.Substring(0, fen.IndexOf(' '));
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
                Console.WriteLine("Invalid FEN notation");
            }
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
        public bool CanMakeMove(int x,int y, bool color)
        {
            if (IsCellClear(x, y) || CanCapture(x, y, color))
                return true;
            else return false;
        }
        public bool CanCapture(int x,int y, bool color)
        {
            if ((color == ChessPiece.WHITE && char.IsLower(Board[x, y])) //IsLower --> black
                || (color == ChessPiece.BLACK && char.IsUpper(Board[x, y]))) //IsUpper --> white
                return true;
            else return false;
        }
        public bool IsCellClear(int x,int y)
        {
            return Board[x, y] == '.';
        }
       
        public List<Move> possibleMoves()
        {
            return null;
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
    }
}

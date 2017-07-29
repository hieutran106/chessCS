using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS
{
    public class Rating
    {
        static int[,] pawnEvalWhite ={//attribute to http://chessprogramming.wikispaces.com/Simplified+evaluation+function
        { 0,  0,  0,  0,  0,  0,  0,  0},
        {50, 50, 50, 50, 50, 50, 50, 50},
        {10, 10, 20, 30, 30, 20, 10, 10},
        { 5,  5, 10, 25, 25, 10,  5,  5},
        { 0,  0,  0, 20, 20,  0,  0,  0},
        { 5, -5,-10,  0,  0,-10, -5,  5},
        { 5, 10, 10,-20,-20, 10, 10,  5},
        { 0,  0,  0,  0,  0,  0,  0,  0}};
        static int[,] pawnEvalBlack = FlipMatrix(pawnEvalWhite);


    static int[,] rookEvalWhite={
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 5, 10, 10, 10, 10, 10, 10,  5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        { 0,  0,  0,  5,  5,  0,  0,  0}};
       static int[,] rookEvalBlack = FlipMatrix(rookEvalWhite);

        static int[,] knightEvalWhite={
        {-50,-40,-30,-30,-30,-30,-40,-50},
        {-40,-20,  0,  0,  0,  0,-20,-40},
        {-30,  0, 10, 15, 15, 10,  0,-30},
        {-30,  5, 15, 20, 20, 15,  5,-30},
        {-30,  0, 15, 20, 20, 15,  0,-30},
        {-30,  5, 10, 15, 15, 10,  5,-30},
        {-40,-20,  0,  5,  5,  0,-20,-40},
        {-50,-40,-30,-30,-30,-30,-40,-50}};
        static int[,] knightEvalBlack = FlipMatrix(knightEvalWhite);

    static int[,] bishopEvalWhite={
        {-20,-10,-10,-10,-10,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        {-10,  5,  5, 10, 10,  5,  5,-10},
        {-10,  0, 10, 10, 10, 10,  0,-10},
        {-10, 10, 10, 10, 10, 10, 10,-10},
        {-10,  5,  0,  0,  0,  0,  5,-10},
        {-20,-10,-10,-10,-10,-10,-10,-20}};
        static int[,] bishopEvalBlack = FlipMatrix(bishopEvalWhite);
    static int[,] queenEvalWhite={
        {-20,-10,-10, -5, -5,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5,  5,  5,  5,  0,-10},
        { -5,  0,  5,  5,  5,  5,  0, -5},
        {  0,  0,  5,  5,  5,  5,  0, -5},
        {-10,  5,  5,  5,  5,  5,  0,-10},
        {-10,  0,  5,  0,  0,  0,  0,-10},
        {-20,-10,-10, -5, -5,-10,-10,-20}};
        static int[,] queenEvalBlack = FlipMatrix(queenEvalWhite);

    static int[,] kingEvalWhite={
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-20,-30,-30,-40,-40,-30,-30,-20},
        {-10,-20,-20,-20,-20,-20,-20,-10},
        { 20, 20,  0,  0,  0,  0, 20, 20},
        { 20, 30, 10,  0,  0, 10, 30, 20}};
        static int[,] kingEvalBlack = FlipMatrix(kingEvalWhite);

        public static int EvaluateBoard(char[,] board)
        {
            int totalEvaluation = 0;
            for (int i=0;i<8;i++)
                for (int j=0;i<8;j++)
                {
                    totalEvaluation = totalEvaluation + GetPieceValue(board, i, j);
                }
            return totalEvaluation;
        }

        public static int[,] FlipMatrix(int[,] matrix)
        {
            int[,] ret = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; i++)
                    ret[i, j] = matrix[7 - i, 7 - j];
            return ret;
        }
        public static int GetPieceValue(char[,] board,int i,int j)
        {
            //return (value of WHITE - value of BLACK)
            int value = 0;
            //WHITE piece is uppercase
            bool isWhite = char.IsUpper(board[i, j]);
            int absoluteValue = GetAbsoluteValue(board[i, j], isWhite, i, j);             
            return (isWhite)?absoluteValue:-absoluteValue;
        }
        private static int GetAbsoluteValue(char piece, bool isWhite, int i,int j)
        {
            int absoluteValue = 0;
            switch (piece)
            {
                case 'P':
                    absoluteValue = 100 +(isWhite?pawnEvalWhite[i,j]:pawnEvalBlack[i,j]);
                    break;
                case 'R':
                    absoluteValue = 500 + (isWhite?rookEvalWhite[i,j]:rookEvalBlack[i,j]);
                    break;
                case 'N':
                    absoluteValue = 300 + (isWhite?knightEvalWhite[i,j]:knightEvalBlack[i,j]);
                    break;
                case 'B':
                    absoluteValue = 300 + (isWhite?bishopEvalWhite[i,j]:bishopEvalBlack[i,j]);
                    break;
                case 'Q':
                    absoluteValue = 900 + (isWhite?queenEvalWhite[i,j]:queenEvalBlack[i,j]);
                    break;
                case 'K':
                    absoluteValue = 9000 + (isWhite?kingEvalWhite[i,j]:kingEvalBlack[i,j]);
                    break;
                default:
                    break;
            }
            return absoluteValue;
        }
    }
}

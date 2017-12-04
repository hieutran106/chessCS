using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessCS.ChessPieces;


namespace ChessCS
{
    public class Evaluation
    {
        private int[,] pawnEvalWhite ={//attribute to http://chessprogramming.wikispaces.com/Simplified+evaluation+function
        { 0,  0,  0,  0,  0,  0,  0,  0},
        {50, 50, 50, 50, 50, 50, 50, 50},
        {10, 10, 20, 30, 30, 20, 10, 10},
        { 5,  5, 10, 25, 25, 10,  5,  5},
        { 0,  0,  0, 20, 20,  0,  0,  0},
        { 5, -5,-10,  0,  0,-10, -5,  5},
        { 5, 10, 10,-20,-20, 10, 10,  5},
        { 0,  0,  0,  0,  0,  0,  0,  0}};
        private int[,] pawnEvalBlack;


        private int[,] rookEvalWhite={
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 5, 10, 10, 10, 10, 10, 10,  5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        { 0,  0,  0,  5,  5,  0,  0,  0}};
        private int[,] rookEvalBlack;

        private int[,] knightEvalWhite={
        {-50,-40,-30,-30,-30,-30,-40,-50},
        {-40,-20,  0,  0,  0,  0,-20,-40},
        {-30,  0, 10, 15, 15, 10,  0,-30},
        {-30,  5, 15, 20, 20, 15,  5,-30},
        {-30,  0, 15, 20, 20, 15,  0,-30},
        {-30,  5, 10, 15, 15, 10,  5,-30},
        {-40,-20,  0,  5,  5,  0,-20,-40},
        {-50,-40,-30,-30,-30,-30,-40,-50}};
        private int[,] knightEvalBlack;

        private int[,] bishopEvalWhite={
        {-20,-10,-10,-10,-10,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        {-10,  5,  5, 10, 10,  5,  5,-10},
        {-10,  0, 10, 10, 10, 10,  0,-10},
        {-10, 10, 10, 10, 10, 10, 10,-10},
        {-10,  5,  0,  0,  0,  0,  5,-10},
        {-20,-10,-10,-10,-10,-10,-10,-20}};
        static int[,] bishopEvalBlack;
        private int[,] queenEvalWhite={
        {-20,-10,-10, -5, -5,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5,  5,  5,  5,  0,-10},
        { -5,  0,  5,  5,  5,  5,  0, -5},
        {  0,  0,  5,  5,  5,  5,  0, -5},
        {-10,  5,  5,  5,  5,  5,  0,-10},
        {-10,  0,  5,  0,  0,  0,  0,-10},
        {-20,-10,-10, -5, -5,-10,-10,-20}};
        private int[,] queenEvalBlack;

        private int[,] kingEvalWhite={
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-20,-30,-30,-40,-40,-30,-30,-20},
        {-10,-20,-20,-20,-20,-20,-20,-10},
        { 20, 20,  0,  0,  0,  0, 20, 20},
        { 20, 30, 10,  0,  0, 10, 30, 20}};
        private int[,] kingEvalBlack;
        public Evaluation()
        {
            pawnEvalBlack = FlipMatrix(pawnEvalWhite);
            rookEvalBlack = FlipMatrix(rookEvalWhite);
            knightEvalBlack = FlipMatrix(knightEvalWhite);
            bishopEvalBlack = FlipMatrix(bishopEvalWhite);
            queenEvalBlack = FlipMatrix(queenEvalWhite);
            kingEvalBlack = FlipMatrix(kingEvalWhite);

        }
        public int EvaluateBoard(ChessBoard chessBoard)
        {
            int x_whiteKing = -1;
            int y_whiteKing = -1;
            int x_blackKing = -1;
            int y_blackKing = -1;
            int totalEvaluation = 0;

            char[,] board = chessBoard.Board;
            for (int i=0;i<8;i++)
                for (int j=0;j<8;j++)
                {
                    totalEvaluation = totalEvaluation + GetPieceValue(board, i, j);
                    if (board[i,j]=='K')
                    {
                        x_whiteKing = i;
                        y_whiteKing = j;
                    } else  if (board[i,j]=='k')
                    {
                        x_blackKing = i;
                        y_blackKing = j;
                    }
                }
            return totalEvaluation;
        }
        
        public int[,] FlipMatrix(int[,] matrix)
        {
            int[,] ret = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    ret[i, j] = matrix[7 - i, 7 - j];
            return ret;
        }
        public int GetPieceValue(char[,] board,int i,int j)
        {
            //return (value of WHITE - value of BLACK)
            //WHITE piece is uppercase
            bool isWhite = char.IsUpper(board[i, j]);
            char piece = char.ToUpper(board[i, j]);
            int absoluteValue = GetAbsoluteValue(piece, isWhite, i, j);
                         
            return (isWhite)?absoluteValue:-absoluteValue;
        }
        private int GetAbsoluteValue(char piece, bool isWhite, int i,int j)
        {
            int absoluteValue = 0;
            switch (piece)
            {
                case 'P':
                    //absoluteValue = 100 +(isWhite?pawnEvalWhite[i,j]:pawnEvalBlack[i,j]);
                    absoluteValue = 100;
                    break;
                case 'R':
                    //absoluteValue = 500 + (isWhite?rookEvalWhite[i,j]:rookEvalBlack[i,j]);
                    absoluteValue = 500;
                    break;
                case 'N':
                    //absoluteValue = 300 + (isWhite?knightEvalWhite[i,j]:knightEvalBlack[i,j]);
                    absoluteValue = 300;
                    break;
                case 'B':
                    //absoluteValue = 300 + (isWhite?bishopEvalWhite[i,j]:bishopEvalBlack[i,j]);
                    absoluteValue = 300;
                    break;
                case 'Q':
                    //absoluteValue = 900 + (isWhite?queenEvalWhite[i,j]:queenEvalBlack[i,j]);
                    absoluteValue = 900;
                    break;
                case 'K':
                    //absoluteValue = 9000 + (isWhite?kingEvalWhite[i,j]:kingEvalBlack[i,j]);
                    absoluteValue = 9000;
                    break;
                default:
                    break;
            }
            return absoluteValue;
        }
    }
}

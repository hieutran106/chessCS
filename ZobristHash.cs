﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS
{
    public class ZobristHash
    {
        ulong[,] table;
        public ZobristHash()
        {
            Random random = new Random(769865452);
            table = new ulong[64, 12];
            for(int i=0;i<64;i++)
                for(int j=0;j<12;j++)
                {
                    table[i, j] = random.NextULong();
                }

        }
        public ulong UpdateHash(ulong oriHash, Move move)
        {
            ulong hash = oriHash;
            //XORing out the piece at source
            return hash;
        }
        public ulong Hash(char[,] board)
        {
            ulong hash = 0UL;
            for (int i=0;i<64;i++)
            {
                int row = i / 8;
                int col = i % 8;
                if (board[row,col]!='.')
                {
                    int j = indexOf(board[row, col]);
                    hash = hash ^ table[i, j];
                }
            }
            return hash;

        }
        // This function associates each piece with
        // a number
        public int indexOf(char piece)
        {
            switch (piece)
            {
                case 'P': return 0;
                case 'N': return 1;
                case 'B': return 2;
                case 'R': return 3;
                case 'Q': return 4;
                case 'K': return 5;
                case 'p': return 6;
                case 'n': return 7;
                case 'b': return 8;
                case 'r': return 9;
                case 'q': return 10;
                case 'k': return 11;
                default: return -1;
            }
        }
    }
}

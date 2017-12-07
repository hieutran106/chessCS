using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCS
{
    public class TranspositionTable
    {
        private static ulong TABLE_SIZE = 100000 * 4;
        public static int VAL_UNKNOWN = -10001;
        private HashEntry[] table;
        public TranspositionTable()
        {
            table = new HashEntry[TABLE_SIZE];
        }
        public void RecordHash(ulong hash, int value, int flag, int depth)
        {
            HashEntry entry = new HashEntry(hash, value, flag, depth);
            int index = (int)(hash % TABLE_SIZE);
            table[index] = entry;
        }
        public int ProbeHash(ulong hash, int depth, int alpha, int beta)
        {
            int index = (int)(hash % TABLE_SIZE);
            HashEntry entry = table[index];
            if (entry!=null )
            {
                if (entry.Hash==hash)
                {
                    if (entry.Depth>=depth)
                    {
                        if (entry.Flag==HashEntry.HASH_EXACT)
                        {
                            return entry.Value;
                        }
                        if ((entry.Flag==HashEntry.HASH_ALPHA)&&(entry.Value <=alpha))
                        {
                            return alpha;
                        }
                        if (entry.Flag==HashEntry.HASH_BETA &&(entry.Value>=beta))
                        {
                            return beta;
                        }
                    }
                }
            }
            return VAL_UNKNOWN;
        }
    }
}

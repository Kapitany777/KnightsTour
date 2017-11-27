using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFKnights
{
    /// <summary>
    /// Egy pozíció koordinátái a sakktáblán
    /// </summary>
    public struct Position
    {
        public int Row;
        public int Column;

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return $"Row: {Row}, Column: {Column}";
        }
    }
}

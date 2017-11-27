using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFKnights
{
    /// <summary>
    /// Egy lehetséges lépés
    /// </summary>
    public struct Move
    {
        /// <summary>
        /// A sorban történő elmozdulás
        /// </summary>
        public int DRow;

        /// <summary>
        /// Az oszlopban történő elmozdulás
        /// </summary>
        public int DColumn;

        public Move(int dRow, int dColumn)
        {
            DRow = dRow;
            DColumn = dColumn;
        }
    }
}

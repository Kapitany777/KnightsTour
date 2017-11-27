using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFKnights
{
    /// <summary>
    /// Adott méretű feladvány megoldása
    /// </summary>
    public class Solver
    {
        /// <summary>
        /// A tábla mérete
        /// </summary>
        private int size;

        /// <summary>
        /// A megtalált útvonal
        /// </summary>
        public Position[] Route { get; private set; }

        public Solver(int size)
        {
            this.size = size;
        }

        /// <summary>
        /// A lépések sorrendjének meghatározása a tábla méretétől függően
        /// </summary>
        /// <returns>A lépések tömbje</returns>
        Move[] GetKnightMoves()
        {
            if (size == 5)
            {
                return new Move[]
                {
                    new Move(-2, -1),
                    new Move(-2, 1),
                    new Move(-1, 2),
                    new Move(1, 2),
                    new Move(2, 1),
                    new Move(2, -1),
                    new Move(1, -2),
                    new Move(-1, -2)
                };
            }

            if (size == 6)
            {
                return new Move[]
                {
                    new Move(2, -1),
                    new Move(1, 2),
                    new Move(2, 1),
                    new Move(1, -2),
                    new Move(-1, -2),
                    new Move(-2, -1),
                    new Move(-2, 1),
                    new Move(-1, 2)
                };
            }

            if (size == 7)
            {
                return new Move[]
                {
                    new Move(2, -1),
                    new Move(1, 2),
                    new Move(2, 1),
                    new Move(1, -2),
                    new Move(-1, -2),
                    new Move(-2, -1),
                    new Move(-2, 1),
                    new Move(-1, 2)
                };
            }

            if (size == 8)
            {
                return new Move[]
                {
                    new Move(1, 2),
                    new Move(2, 1),
                    new Move(2, -1),
                    new Move(1, -2),
                    new Move(-1, -2),
                    new Move(-2, -1),
                    new Move(-2, 1),
                    new Move(-1, 2)
                };
            }

            return null;
        }

        /// <summary>
        /// A feladvány megoldása
        /// </summary>
        /// <returns>Sikerült-e megoldást találni?</returns>
        public bool Solve()
        {
            Move[] knightMoves = GetKnightMoves();

            if (knightMoves == null)
            {
                return false;
            }

            int moveCount = knightMoves.Length;
            int squareCount = size * size;

            bool[,] board = new bool[size, size];

            Route = new Position[squareCount];

            Position pos = new Position(0, 0);
            board[pos.Row, pos.Column] = true;
            Route[0] = pos;

            int[] moveNumber = new int[squareCount];

            int routeLength = 0;
            int tmpRow;
            int tmpColumn;

            while (routeLength >= 0 && routeLength < squareCount - 1)
            {
                tmpRow = pos.Row + knightMoves[moveNumber[routeLength]].DRow;
                tmpColumn = pos.Column + knightMoves[moveNumber[routeLength]].DColumn;

                if (tmpRow >= 0 && tmpRow < size &&
                    tmpColumn >= 0 && tmpColumn < size &&
                    !board[tmpRow, tmpColumn])
                {
                    board[tmpRow, tmpColumn] = true;
                    routeLength++;
                    pos.Row = tmpRow;
                    pos.Column = tmpColumn;
                    Route[routeLength] = pos;
                    moveNumber[routeLength] = 0;
                }
                else
                {
                    if (moveNumber[routeLength] < moveCount - 1)
                    {
                        moveNumber[routeLength]++;
                    }
                    else
                    {
                        while (routeLength >= 0 && moveNumber[routeLength] == moveCount - 1)
                        {
                            board[Route[routeLength].Row, Route[routeLength].Column] = false;
                            routeLength--;
                        }

                        if (routeLength >= 0)
                        {
                            moveNumber[routeLength]++;
                            pos = Route[routeLength];
                        }
                    }
                }
            }

            return (routeLength > 0);
        }
    }
}

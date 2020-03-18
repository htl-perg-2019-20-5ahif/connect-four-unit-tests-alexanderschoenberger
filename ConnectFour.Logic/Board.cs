using System;
using System.Data;
using System.Linq;

namespace ConnectFour.Logic
{
    public class Board
    {
        /// <summary>
        /// [Column, Row]
        /// [0..6, 0..5]
        /// </summary>
        private readonly byte[,] GameBoard = new byte[7, 6];

        /// <summary>
        /// The current player (always modulo 2).
        /// </summary>
        internal int Player = 0;

        /// <summary>
        /// Adds a stone to the specified column. 
        /// </summary>
        /// <param name="column">The column where the stone should be added.</param>
        public void AddStone(byte column)
        {
            if (column > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            for (int row = 0; row < 6; row++)
            {
                var cell = GameBoard[column, row];

                if (cell == 0)
                {
                    Player = (Player + 1) % 2;
                    GameBoard[column, row] = (byte)(Player + 1);
                    return;
                }
            }

            throw new InvalidOperationException("Column is full");
        }

        public int getCurrentPlayer()
        {
            return Player;
        }

        /// <summary>
        /// 0: Game isn't over
        /// 1: board is full
        /// 2: Player 1
        /// 3: Player 2
        /// </summary>
        public int HasGameEnded()
        {
            if (IsBoardFull())
            {
                return 1;
            }
            var player = GetHorizontalWinner();
            if (player == -1)
            {
                player = GetVerticalWinner();
                if (player == -1)
                {
                    player = GetDiagonalWinner();
                    if (player == -1)
                    {
                        return 0;
                    }
                }
            }
            return player;
        }

        public int GetHorizontalWinner()
        {
            var columnLength = GameBoard.GetLength(0);
            var rowLength = GameBoard.GetLength(1);

            for (byte row = 0; row < rowLength; row++)
            {
                byte count = 0;
                for (byte column = 1; column < columnLength; column++)
                {

                    if (GameBoard[column - 1, row] == GameBoard[column, row] && GameBoard[column, row] != 0)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }

                    if (count == 3)
                    {
                        return GameBoard[column, row];
                    }
                }
            }

            return -1;
        }

        public int GetVerticalWinner()
        {
            var columnLength = GameBoard.GetLength(0);
            var rowLength = GameBoard.GetLength(1);
            for (byte column = 0; column < columnLength; column++)
            {
                byte count = 0;
                for (byte row = 1; row < rowLength; row++)
                {
                    if (GameBoard[column, row - 1] == GameBoard[column, row] && GameBoard[column, row] != 0)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }

                    if (count == 3)
                    {
                        return GameBoard[column, row];
                    }
                }
            }

            return -1;
        }

        public int GetDiagonalWinner()
        {
            var columnLength = GameBoard.GetLength(0);
            var rowLength = GameBoard.GetLength(1);
            // Left to right
            for (byte column = 0; column < columnLength; column++)
            {
                byte count = 0;
                byte columnRow = 0;
                for (byte row = 0; row < rowLength - 1 && columnRow < rowLength - 1; row++, columnRow++)
                {
                    if (GameBoard[columnRow, row] == GameBoard[columnRow + 1, row + 1] && GameBoard[columnRow, row] != 0)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }

                    if (count == 3)
                    {
                        return GameBoard[columnRow, row];
                    }
                }
            }
            //right to left
            for (byte column = (byte)(columnLength - 1); column > 0; column--)
            {
                byte count = 0;
                byte columnRow = column;
                for (byte row = 0; row < rowLength - 1 && columnRow > 0; row++, columnRow--)
                {
                    if (GameBoard[columnRow, row] == GameBoard[columnRow - 1, row + 1] && GameBoard[columnRow, row] != 0)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }

                    if (count == 3)
                    {
                        return GameBoard[columnRow, row];
                    }
                }
            }
            return default;
        }

        public bool IsBoardFull()
        {
            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                var maxRow = GameBoard.GetLength(1) - 1;
                if (GameBoard[i, maxRow] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void printBoard()
        {
            var columnLength = GameBoard.GetLength(0);
            var rowLength = GameBoard.GetLength(1);


            for (byte row = 0; row < rowLength; row++)
            {
                for (byte column = 0; column < columnLength; column++)
                {

                    Console.Write(GameBoard[column, row]);
                }
                Console.WriteLine();
            }
        }
    }
}

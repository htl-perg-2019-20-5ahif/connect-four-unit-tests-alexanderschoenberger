using ConnectFour.Logic;
using System;

namespace ConnectFour.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start game");
            Board b = new Board();
            bool GameIsRunning = true;
            int value;
            do
            {
                Console.WriteLine($"Player {b.getCurrentPlayer() + 1} please insert stone");
                var column = byte.Parse(Console.ReadLine());

                try
                {
                    b.AddStone(column);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                b.printBoard();
                value = b.HasGameEnded();
                if (value != 0)
                {
                    GameIsRunning = false;
                }
            }
            while (GameIsRunning);
            if (value == 1)
            {
                Console.WriteLine("Board is full");
            }
            else
            {
                Console.WriteLine($"Player {value - 1} has won!");
            }
        }
    }
}

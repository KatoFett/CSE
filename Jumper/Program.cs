using System;

namespace Jumper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var puzzle = new Puzzle();
            while (!puzzle.IsPuzzleOver)
            {
                Console.WriteLine(puzzle);
                Console.Write("Guess a letter [a-z]: ");
                var input = Console.ReadLine();
                if(input?.Length > 0)
                    puzzle.Guess(input[0]);
                Console.WriteLine();
            }
            Console.WriteLine(puzzle);
            Console.WriteLine(puzzle.IsPuzzleWon ? "Congratulations!" : "Maybe next time!");
        }
    }
}
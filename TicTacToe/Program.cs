// Program: Tic Tac Toe
// Author: Aaron Fox

namespace TicTacToe
{

    using System;
    using System.Linq;

    public class Program
    {
        enum Player
        {
            Undefined,
            X,
            O
        }

        static void Main(string[] args)
        {
            Player[] squares = new Player[9];

            Player winner = Player.Undefined;
            var isTie = false;
            var currentPlayer = Player.X;

            while (winner == Player.Undefined && !isTie)
                winner = NextTurn(squares, currentPlayer, out currentPlayer, out isTie);

            PrintBoard(squares);

            Console.WriteLine($"{(winner == Player.Undefined ? "It's a tie" : winner)}, good game. Thanks for playing!");
        }

        /// <summary>
        /// Prints the given board on the screen.
        /// </summary>
        /// <remarks>
        /// You are going to LOVE the unnecessary work I put into this one. :)
        /// </remarks>
        static void PrintBoard(Player[] squares)
        {
            // Pfft, who would use extremely simple for-loops?
            // LINQ is the way of the future!
            var boardString = squares
                // Using this overload because IndexOf always returns 0 when the array is all nulls.
                .Select((square, index) => new { square, index })
                // Group squares by every three.
                .GroupBy(obj => obj.index / 3)
                .Select(group => group
                    // Put the player's square or the square's index if unfilled.
                    .Select(obj => (obj.square == Player.Undefined
                        ? obj.index + 1
                        : (object)obj.square).ToString())
                    // Insert vertical separators.
                    .Aggregate((a, b) => $"{a}|{b}")
                )
                // Add horizontal separators and newline characters.
                .Aggregate((a, b) => $"{a}{Environment.NewLine}-+-+-{Environment.NewLine}{b}");

            Console.WriteLine(boardString);
            Console.WriteLine();
        }


        /// <summary>
        /// Handles the turn of the given player.
        /// </summary>
        static Player NextTurn(Player[] squares, Player currentPlayer, out Player nextPlayer, out bool isTie)
        {
            PrintBoard(squares);

            int selectedSquare;
            string? squareChar;

            do
            {
                Console.Write($"{currentPlayer}'s turn to choose a square (1-9): ");
                squareChar = Console.ReadLine();
                Console.WriteLine();
            }
            while (!IsValidSquare(squares, squareChar, out selectedSquare));

            squares[selectedSquare - 1] = currentPlayer;

            // Swap to other player.
            nextPlayer = (currentPlayer & (Player)1) + 1;

            return GetWinner(squares, out isTie);
        }

        /// <summary>
        /// Returns whether or not the <paramref name="squareChar"/> is a valid, open square.
        /// </summary>
        static bool IsValidSquare(Player[] squares, string? squareChar, out int square)
        {
            if (int.TryParse(squareChar, out square) && square > 0 && square <= 9)
                return squares[square - 1] == Player.Undefined;
            return false;
        }

        /// <summary>
        /// Determines a winner (if any) of a given board.
        /// </summary>
        /// <remarks>
        /// Please read the for-loops. Please.
        /// </remarks>
        static Player GetWinner(Player[] squares, out bool isTie)
        {
            isTie = false;

            // Row winner.
            // I bet you've never seen a for-loop like this. ;)
            // Pray that you never do again.
            for (string row = ""; row != "aaa"; row += "a")
            {
                var startCol = row.Length * 3;
                var winner = squares[startCol] & squares[startCol + 1] & squares[startCol + 2];
                if (winner != Player.Undefined) return winner;
            }

            // Column winner.
            // At this point I'm honestly just having fun.
            // Yes, I thoroughly enjoying writing working code that
            // makes you question your/my sanity.
            for (int colFlag = 1; colFlag != 1 << 3; colFlag <<= 1)
            {
                var startCol = (int)Math.Log(colFlag, 2);
                var winner = squares[startCol] & squares[startCol + 3] & squares[startCol + 6];
                if (winner != Player.Undefined) return winner;
            }

            // Top-left diagonal.
            if ((squares[0] & squares[4] & squares[8]) != Player.Undefined)
                return squares[0];

            // Top-right diagonal.
            if ((squares[2] & squares[4] & squares[6]) != Player.Undefined)
                return squares[2];

            isTie = !squares.Any(square => square == Player.Undefined);

            return Player.Undefined;
        }
    }
}
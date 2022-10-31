namespace CSE_210_01.Dice
{
    /// <summary>
    /// Initiates and manages a game of Dice.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Converts a dice <paramref name="roll"/> into its point value.
        /// </summary>
        /// <param name="roll">The dice roll to convert to points.</param>
        /// <returns>An integer value representing the value in points.</returns>
        static int GetRollPoints(int roll)
        {
            return roll switch
            {
                1 => 100,
                5 => 50,
                _ => 0
            };
        }

        /// <summary>
        /// The method called when the program starts.
        /// </summary>
        /// <param name="args">Args are not used in this program.</param>
        static void Main(string[] args)
        {
            var score = 0;
            var canRollAgain = true;

            do
            {
                Console.Write("Roll dice? [y/n] ");
                var rollAgain = Console.ReadLine()?.ToLower() == "y";
                if (rollAgain)
                {
                    var rolls = Enumerable
                        .Repeat(0, 5)
                        .Select(r => Die.Roll())
                        .ToList();

                    var points = rolls
                        .Select(r => GetRollPoints(r))
                        .Sum();

                    // To display to user.
                    var rollStr = rolls
                        .Select(r => r.ToString())
                        .Aggregate((a, b) => $"{a} {b}");

                    if (points == 0) canRollAgain = false;
                    else score += points;

                    Console.WriteLine($"You rolled: {rollStr}");
                    Console.WriteLine($"Your score is: {score}");
                    Console.WriteLine();
                }
                else canRollAgain = false;
            }
            while (canRollAgain);

            Console.WriteLine($"Final score: {score}");
        }
    }
}
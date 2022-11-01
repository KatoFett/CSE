namespace Hilo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dealer = new Dealer();
            var score = 300;

            do
            {
                var firstCard = dealer.PullCard();
                var nextCard = dealer.PullCard();

                Console.WriteLine();
                WriteCard(firstCard);

                Console.Write("Higher or lower? [h/l] ");
                var higher = Console.ReadLine()?.ToLower() == "h";

                WriteCard(nextCard);

                score += higher && nextCard > firstCard
                    || !higher && nextCard < firstCard
                    ? 100
                    : -75;

                Console.WriteLine($"Your score is: {score}");

                if (score > 0)
                {
                    Console.Write("Play again? [y/n] ");
                    var playAgain = Console.ReadLine()?.ToLower() == "y";
                    if(playAgain)
                        dealer.ResetDeck();
                    else
                        score = 0;
                }

            } while (score > 0);
        }

        /// <summary>
        /// Prints a card to the console.
        /// </summary>
        /// <param name="card">The card to be printed.</param>
        static void WriteCard(Card card)
        {
            Console.WriteLine($"The card is: {card}");
        }
    }
}
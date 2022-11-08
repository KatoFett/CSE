using System;
using System.Collections.Generic;
using System.Linq;

namespace Jumper
{
    /// <summary>
    /// A puzzle with a <see cref="Player"/> and a random <see cref="Word"/> to guess.
    /// </summary>
    internal class Puzzle
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Puzzle"/> class with a new <see cref="Player"/>
        /// and a new <see cref="Word"/>.
        /// </summary>
        internal Puzzle()
        {
            Player = new Player(this);
            Word = new Word(this, AvailableWords[Random.Shared.Next(0, AvailableWords.Length)]);
        }

        /// <summary>
        /// All the words that the <see cref="Puzzle"/> may use.
        /// </summary>
        internal string[] AvailableWords { get; } = new string[]
        {
            "Class",
            "Programming",
            "Program",
            "Code",
            "Abstraction",
            "Encapsulation",
            "Scope",
            "Access",
            "Developer"
        };

        /// <summary>
        /// The letters guessed by the <see cref="Player"/>.
        /// </summary>
        internal List<char> Guesses { get; } = new();

        /// <summary>
        /// The player.
        /// </summary>
        internal Player Player { get; }

        /// <summary>
        /// The word to be guessed by the <see cref="Player"/>.
        /// </summary>
        internal Word Word { get; }

        /// <summary>
        /// Whether the player successfully guessed the word.
        /// </summary>
        internal bool IsPuzzleWon => _IsPuzzleWon;
        private bool _IsPuzzleWon;

        /// <summary>
        /// Whether the puzzle should be terminated.
        /// </summary>
        internal bool IsPuzzleOver => Player.Health <= 0 || _IsPuzzleWon;

        /// <summary>
        /// Guesses a letter in the <see cref="Word"/>, ignoring case.
        /// </summary>
        /// <remarks>
        /// If the <see cref="Player"/> guesses incorrectly, they take damage.
        /// If the letter was already guessed, nothing happens.
        /// </remarks>
        /// <param name="guess">The letter that was guessed.</param>
        internal void Guess(char guess)
        {
            var charLower = char.ToLower(guess);
            if (Guesses.Contains(charLower))
                return;

            Guesses.Add(charLower);

            if (!Word.Value.Contains(guess, StringComparison.InvariantCultureIgnoreCase))
                Player.Damage();
            else if (Word.Value.ToLower().Distinct().All(w => Guesses.Contains(w)))
                _IsPuzzleWon = true;
        }

        /// <summary>
        /// Gets a visual display of this <see cref="Puzzle"/>.
        /// </summary>
        internal string GetDisplay()
        {
            return $"{Word}{Environment.NewLine}{Environment.NewLine}" +
                $"{Player}{Environment.NewLine}{Environment.NewLine}" +
                "^^^^^^^";
        }

        public override string ToString()
        {
            return GetDisplay();
        }
    }
}

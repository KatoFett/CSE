using System.Collections.Generic;
using System.Linq;

namespace Jumper
{
    /// <summary>
    /// The word that to be guessed by the player.
    /// </summary>
    internal class Word
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Word"/> class.
        /// </summary>
        /// <param name="value">The word to be guessed.</param>
        internal Word(Puzzle puzzle, string value)
        {
            Puzzle = puzzle;
            Value = value;
        }

        /// <summary>
        /// The separator between each letter of the word.
        /// This is for display purposes only.
        /// </summary>
        const string CHAR_SEPARATOR = " ";

        /// <summary>
        /// The puzzle associated with this <see cref="Word"/>.
        /// </summary>
        internal Puzzle Puzzle { get; }

        /// <summary>
        /// The word to be guessed by the player.
        /// </summary>
        internal string Value { get; }

        /// <summary>
        /// Gets the display for the word with the current guesses.
        /// </summary>
        /// <param name="guessedLetters">
        /// The letters guessed by the player.
        /// Case is ignored.
        /// </param>
        internal string GetWordDisplay(IEnumerable<char> guessedLetters)
        {
            return Value
                .Select(c =>
                    guessedLetters.Any(g => char.ToLower(g) == char.ToLower(c))
                    ? c
                    : '_')
                .Select(c => c.ToString())
                .Aggregate((a, b) => $"{a}{CHAR_SEPARATOR}{b}");
        }

        public override string ToString()
        {
            return GetWordDisplay(Puzzle.Guesses);
        }
    }
}

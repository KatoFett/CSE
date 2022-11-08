using System;
using System.Collections.Generic;
using System.Linq;

namespace Jumper
{
    /// <summary>
    /// Represents the player and their lives.
    /// </summary>
    internal class Player
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="puzzle">The <see cref="Puzzle"/> associated with the <see cref="Player"/>.</param>
        internal Player(Puzzle puzzle)
        {
            Puzzle = puzzle;
        }

        /// <summary>
        /// The <see cref="Puzzle"/> associated with the <see cref="Player"/>.
        /// </summary>
        internal Puzzle Puzzle { get; }

        /// <summary>
        /// Gets a value from 0 to 4 representing the <see cref="Player"/>'s health.
        /// </summary>
        internal int Health => _Health;
        private int _Health = 4;

        /// <summary>
        /// Reduces the <see cref="Player"/>'s health by 1.
        /// </summary>
        internal void Damage()
        {
            if (Health <= 0)
                throw new InvalidOperationException("Cannot damage a dead player.");

            _Health--;
        }

        /// <summary>
        /// Creates an ASCII display of the <see cref="Player"/>.
        /// The display depends on their health.
        /// </summary>
        internal string GetDisplay()
        {
            var retval = string.Empty;

            if (Health >= 4)
                retval += $"  ___{Environment.NewLine}";
            if (Health >= 3)
                retval += $" /___\\{Environment.NewLine}";
            if (Health >= 2)
                retval += $" \\   /{Environment.NewLine}";
            if (Health >= 1)
                retval += $"  \\ /{Environment.NewLine}";

            retval += $"   {(Health == 0 ? 'X' : 'O')}{Environment.NewLine}"
                + $"  /|\\{Environment.NewLine}"
                + "  / \\";

            return retval;
        }

        public override string ToString()
        {
            return GetDisplay();
        }
    }
}

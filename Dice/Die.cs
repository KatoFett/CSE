namespace Dice
{
    using System;

    /// <summary>
    /// Represents a virtual 6-sided die.
    /// </summary>
    internal class Die
    {
        /// <summary>
        /// Rolls a die and returns the rolled value.
        /// </summary>
        /// <returns>An integer representing the value rolled.</returns>
        internal static int Roll()
        {
            return Random.Shared.Next(1, 6);
        }
    }
}

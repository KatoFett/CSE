namespace CSE_210_01.Dice
{
    using System;

    /// <summary>
    /// Represents a virtual die.
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

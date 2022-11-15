using System;

namespace GameEngine
{
    /// <summary>
    /// Helper methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Rounds the <see cref="float"/> to the nearest whole <see cref="int"/>.
        /// </summary>
        /// <param name="f">The float to round.</param>
        /// <returns>A new <see cref="int"/> with the rounded value.</returns>
        public static int ToInt(this float f)
        {
            return (int)Math.Round(f, 0);
        }

        /// <summary>
        /// Returns a random <see cref="float"/> that's at least <paramref name="minValue"/> and less than <paramref name="maxValue"/>.
        /// </summary>
        /// <param name="minValue">The minimum value inclusive.</param>
        /// <param name="maxValue">The maximum value exclusive.</param>
        public static float NextSingle(this Random rand, float minValue, float maxValue)
        {
            return minValue + (rand.NextSingle() * (maxValue - minValue));
        }
    }
}

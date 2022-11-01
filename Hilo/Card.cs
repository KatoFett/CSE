namespace Hilo
{
    using System;

    /// <summary>
    /// Represents a playing card.
    /// </summary>
    internal class Card
    {
        /// <summary>
        /// Creates a new instance of a <see cref="Card"/>
        /// with a <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value of the card from 1-13.</param>
        public Card(int value)
        {
            if (value < 1 || value > 13)
                throw new ArgumentOutOfRangeException(nameof(value));
            Value = value;
        }

        /// <summary>
        /// Gets the value of the <see cref="Card"/> from 1-13.
        /// </summary>
        public int Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool operator >(Card c1, Card c2)
        {
            return c1.Value > c2.Value;
        }

        public static bool operator <(Card c1, Card c2)
        {
            return c1.Value < c2.Value;
        }
    }
}

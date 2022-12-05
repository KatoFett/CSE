using GameEngine;
using System;
using System.Collections.Generic;

namespace Solitaire
{
    /// <summary>
    /// A playing card.
    /// </summary>
    public class Card : Sprite
    {
        /// <summary>
        /// Creates a new instance of a <see cref="Card"/>.
        /// </summary>
        /// <param name="suit">The suit the card is a part of.</param>
        /// <param name="value">The card's value from 1 (Ace) to 14 (Joker)</param>
        public Card(CardSuit suit, int value) : base()
        {
            if (value < 1 || value > 14)
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value)} must be between 1 and 14.");
            Suit = suit;
            Value = value;
            UpdateTextureName();
        }

        /// <summary>
        /// A card suit.
        /// </summary>
        public enum CardSuit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        /// <summary>
        /// The suit this card is a part of.
        /// </summary>
        public CardSuit Suit { get; }

        /// <summary>
        /// The value of the card from 1 (Ace) to 14 (Joker).
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Gets or sets whether the card is face down.
        /// </summary>
        public bool IsFaceDown
        {
            get => _IsFaceDown;
            set
            {
                _IsFaceDown = value;
                UpdateTextureName();
            }
        }
        private bool _IsFaceDown = true;

        /// <summary>
        /// Gets or sets the lane the card is in.
        /// </summary>
        public List<Card>? Lane { get; set; }

        private void UpdateTextureName()
        {
            TextureName = IsFaceDown
                ? "card_back"
                : $"card_{Suit.ToString().ToLowerInvariant()}{Value}";
        }
    }
}

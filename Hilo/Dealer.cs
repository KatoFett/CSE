namespace Hilo
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A manager for handling a deck of <see cref="Card"/>s.
    /// </summary>
    internal class Dealer
    {
        /// <summary>
        /// Creates a new instance of a <see cref="Dealer"/>
        /// with a deck of <see cref="Card"/>s.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Dealer()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ResetDeck();
        }

        private List<Card> _Deck;

        /// <summary>
        /// Pulls a <see cref="Card"/> from the <see cref="Deck"/>,
        /// removing it in the process.
        /// </summary>
        /// <returns></returns>
        public Card PullCard()
        {
            if (_Deck.Count == 0)
                throw new InvalidOperationException("Cannot pull from an empty deck.");

            var cardIdx = Random.Shared.Next(0, _Deck.Count - 1);
            Card card = _Deck[cardIdx];
            _Deck.RemoveAt(cardIdx);
            return card;
        }

        /// <summary>
        /// Resets the <see cref="Dealer"/>'s <see cref="Deck"/>
        /// with a new set of <see cref="Card"/>s.
        /// </summary>
        public void ResetDeck()
        {
            _Deck = new List<Card>(13);
            for (int i = 1; i <= 13; i++)
                _Deck.Add(new Card(i));
        }
    }
}

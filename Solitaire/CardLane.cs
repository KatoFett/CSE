using GameEngine;
using System;
using System.Linq;
using System.Numerics;

namespace Solitaire
{
    /// <summary>
    /// A lane of cards spread vertically.
    /// </summary>
    public class CardLane : CardStack
    {

        /// <summary>
        /// Creates a new <see cref="CardLane"/>.
        /// </summary>
        /// <param name="position">The starting position of the card lane.</param>
        public CardLane(Vector2 position) : base(position) { }

        public const float VERTICAL_SPACING = 45f;

        public override void AddCard(Card card)
        {
            AddCard(card, true);
        }

        public void AddCard(Card card, bool setFaceUp)
        {
            card.ZIndex = Stack.Select(c => c.ZIndex).DefaultIfEmpty(0).Max(z => z) + 1;
            if (setFaceUp)
            {
                card.IsFaceDown = false;
                card.CanGrab = true;
            }
            card.IsVisible = true;
            card.OwnerStack = this;

            card.Move(GetNextCardPosition(), resetZIndex: false);
            Stack.Push(card);
        }

        public override Vector2 GetNextCardPosition()
        {
            return Position + Stack.Count * new Vector2(0, VERTICAL_SPACING);
        }

        public override void RemoveCard(Card card)
        {
            if (!Stack.Contains(card)) throw new IndexOutOfRangeException();
            while (Stack.Pop() != card) { }
            if (Stack.TryPeek(out var nextCard))
            {
                nextCard.IsFaceDown = false;
                nextCard.CanGrab = true;
            }
        }
    }
}

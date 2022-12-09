using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    /// <summary>
    /// A stack of cards of the same suit.
    /// </summary>
    public class SuitStack : CardStack
    {
        public SuitStack(Vector2 position) : base(position)
        {
            TextureName = "card_placeholder";
            Tint = new(255, 255, 255, 63);
        }

        public override void AddCard(Card card)
        {
            if(Stack.TryPeek(out var topCard))
            {
                topCard.IsVisible = false;
            }
            Stack.Push(card);
            card.CanGrab = false;
            card.Move(GetCardPosition());
        }

        public override Vector2 GetCardPosition(Card? card = null)
        {
            return Position;
        }

        public override void RemoveCard(Card card)
        {
            throw new InvalidOperationException();
        }

        public bool CanAcceptCard(Card card)
        {
            return
            // First card of any stack.
            (!Stack.Any() && card.Value == 1)

            // Next card of specific stack.
            || (Stack.TryPeek(out var topCard) && topCard.Suit == card.Suit && topCard.Value == card.Value - 1);
        }
    }
}

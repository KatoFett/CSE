using GameEngine;
using Raylib_cs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    /// <summary>
    /// The deck of cards sitting in the top-left corner of the game.
    /// </summary>
    public class CardDeck : Sprite
    {
        public CardDeck(Vector2 position) : base()
        {
            Position = position;
            InitDeck();
        }

        public const int HAND_SIZE = 3;
        public const float CARD_SPACING = 30f;
        public const float CLICK_DELAY = 0.1f;
        public const float CARD_DELAY = 0.05f;

        /// <summary>
        /// Gets the cards in the deck.
        /// </summary>
        public List<Card> Cards { get; private set; }

        /// <summary>
        /// Gets the cards that are shown to the player.
        /// </summary>
        public List<Card> VisibleCards { get; } = new List<Card>();

        private bool _CanClick = true;

        /// <summary>
        /// Draws and removes the top card from the deck.
        /// </summary>
        /// <returns>The drawn card.</returns>
        public Card DrawCard()
        {
            var card = Cards.Last();
            Cards.Remove(card);
            return card;
        }

        protected internal override void OnMouseDown()
        {
            if (!((MainScene)Scene.ActiveScene).IsInitialized || !_CanClick || !Cards.Any()) return;

            _CanClick = false;

            if (VisibleCards.Count == Cards.Count)
            {
                Scene.StartCoroutine(BringVisibleCardsBack());
            }
            else
            {
                VisibleCards.TakeLast(HAND_SIZE).ToList().ForEach(c =>
                {
                    c.IsVisible = false;
                    c.CanGrab = false;
                });

                var cardCount = Math.Min(HAND_SIZE, Cards.Count - VisibleCards.Count);
                for (int i = 0; i < cardCount; i++)
                {
                    var card = Cards[VisibleCards.Count];
                    VisibleCards.Add(card);
                }

                Scene.StartCoroutine(MoveTopCards());

                if (VisibleCards.Count == Cards.Count)
                {
                    TextureName = "card_placeholder";
                    Tint = new Color(255, 255, 255, 63);
                }
            }
        }

        public void RemoveTopCard()
        {
            var topCard = VisibleCards.Last();
            VisibleCards.Remove(topCard);
            Cards.Remove(topCard);
            Scene.StartCoroutine(MoveTopCards(isShifting: true));

            // Cards won't always be shifted, may need to manually set CanGrab.
            if (VisibleCards.Any() )
                VisibleCards.Last().CanGrab = true;
        }

        public void Clear()
        {
            VisibleCards.Clear();
            foreach (var card in Cards)
            {
                card.Dispose();
            }
            Cards.Clear();
        }

        public void InitDeck()
        {
            SetTextureToDefault();
            var cards = new List<Card>();
            for (int suit = 0; suit < 4; suit++)
            {
                for (int value = 0; value < 13; value++)
                {
                    var card = new Card((Card.CardSuit)suit, value + 1)
                    {
                        IsVisible = false,
                        IsFaceDown = true,
                        Position = Position
                    };

                    cards.Add(card);
                }
            }

            Cards = cards.OrderBy(c => Random.Shared.Next()).ToList();
        }

        private IEnumerator MoveTopCards(bool isShifting = false)
        {
            var lastCards = VisibleCards.TakeLast(HAND_SIZE).ToArray();
            var cardCount = Math.Min(HAND_SIZE, VisibleCards.Count);

            for (int i = 0; i < cardCount; i++)
            {
                var card = lastCards[isShifting ? lastCards.Length - i - 1 : i];

                card.IsVisible = true;
                card.IsFaceDown = false;
                card.ZIndex = isShifting ? HAND_SIZE - i : i;

                card.Move(GetCardPosition(card), false);

                yield return new WaitForSeconds(CARD_DELAY);
            }

            yield return new WaitForSeconds(CLICK_DELAY);
            _CanClick = true;

            if(VisibleCards.Any())
                VisibleCards.Last().CanGrab = true;
        }

        public Vector2 GetCardPosition(Card card)
        {
            if (!VisibleCards.Contains(card)) throw new ArgumentOutOfRangeException(nameof(card));
            // Default position is far right.
            var cardX = Position.X + Card.CardSize.X + (Math.Min(HAND_SIZE, VisibleCards.Count) + 1) * CARD_SPACING;
            cardX -= (CARD_SPACING * (VisibleCards.Count - VisibleCards.IndexOf(card)));
            return new Vector2(cardX, Position.Y);
        }

        private IEnumerator BringVisibleCardsBack()
        {
            SetTextureToDefault();
            var lastCards = VisibleCards.TakeLast(HAND_SIZE).Reverse().ToArray();
            var cardCount = Math.Min(HAND_SIZE, VisibleCards.Count);

            for (int i = 0; i < cardCount; i++)
            {
                var card = lastCards[i];
                card.IsVisible = true;
                card.IsFaceDown = true;
                card.ZIndex = 100 + i;
                card.CanGrab = false;
                card.Move(Position);
                yield return new WaitForSeconds(CARD_DELAY);
                card.IsVisible = false;
            }

            foreach (var card in VisibleCards)
            {
                card.IsFaceDown = true;
                card.IsVisible = false;
                card.ZIndex = 0;
                card.Position = Position;
            }

            VisibleCards.Clear();

            yield return new WaitForSeconds(CLICK_DELAY);
            _CanClick = true;
        }

        private void SetTextureToDefault()
        {
            TextureName = "card_back";
            Tint = new Color(255, 255, 255, 255);
        }
    }
}

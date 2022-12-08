using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;

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
        /// A card color.
        /// </summary>
        public enum CardColor
        {
            Black,
            Red
        }

        /// <summary>
        /// The time in seconds it takes for the card to move to a location.
        /// </summary>
        public const float MOVEMENT_TIME = 0.05f;

        /// <summary>
        /// Gets the size of the card's texture.
        /// </summary>
        public static Vector2 CardSize => _CardSize;
        private static Vector2 _CardSize = Vector2.Zero;

        /// <summary>
        /// The suit this card is a part of.
        /// </summary>
        public CardSuit Suit { get; }

        public CardColor Color => Suit switch
        {
            CardSuit.Diamonds => CardColor.Red,
            CardSuit.Hearts => CardColor.Red,
            _ => CardColor.Black
        };

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
        /// Gets or sets whether the card can be picked up by the player.
        /// </summary>
        public bool CanGrab { get; set; }

        private Vector2? _MouseOffset = null;
        private Vector2? _OriginalPosition = null;
        private int _OriginalZIndex;

        /// <summary>
        /// Gets or sets the stack containing the card.
        /// </summary>
        public CardStack? OwnerStack { get; set; }

        /// <summary>
        /// Updates the card's texture name depending on whether it is face down.
        /// </summary>
        private void UpdateTextureName()
        {
            TextureName = IsFaceDown
                ? "card_back"
                : $"card_{Suit.ToString().ToLowerInvariant()}{Value}";
        }

        /// <summary>
        /// Moves and animates the card to a new <paramref name="location"/> on the screen.
        /// </summary>
        /// <param name="location">The card's destination.</param>
        /// <param name="resetZIndex">Whether the card's z-index should be reset.</param>
        public void Move(Vector2 location, bool resetZIndex = true)
        {
            var _ = new Vector2Animation
            (
                Position,
                location,
                (value) => Position = value,
                MOVEMENT_TIME,
                callback: resetZIndex ? (() => ZIndex = _OriginalZIndex) : null
            );
        }

        /// <summary>
        /// Reads the card texture and sets the <see cref="CardSize"/>.
        /// </summary>
        public static void SetSize()
        {
            var cardTexture = VideoService.GetTexture("card_back");
            _CardSize = new Vector2(cardTexture.width, cardTexture.height);
        }

        protected internal override void Update()
        {
            if (_MouseOffset != null)
            {
                Position = MouseService.GetMouseCoordinates() - _MouseOffset.Value;
            }
        }

        protected internal override void OnMouseDown()
        {
            if (IsFaceDown || !CanGrab || !((MainScene)Scene.ActiveScene).IsInitialized) return;
            if (OwnerStack != null)
            {
                var allCards = OwnerStack.Stack.ToList();
                var thisIdx = allCards.IndexOf(this);
                for (int i = thisIdx; i >= 0; i--)
                {
                    allCards[i].PickUp(100 - i);
                }
            }
            else
            {
                PickUp(100);
            }
        }

        protected internal override void OnMouseUp()
        {
            if (_MouseOffset == null) return;
            if (OwnerStack != null)
            {
                var allCards = OwnerStack.Stack.ToList();
                var thisIdx = allCards.IndexOf(this);
                for (int i = thisIdx; i >= 0; i--)
                {
                    allCards[i].SetDown();
                }
            }
            else
            {
                SetDown();
            }
        }

        private void MoveToNewStack(CardStack newOwner)
        {
            if (OwnerStack == null)
            {
                // This is the top card from the drawn hand.
                ((MainScene)Scene.ActiveScene).Deck.RemoveTopCard();
            }
            else
            {
                OwnerStack.RemoveCard(this);
            }

            newOwner.AddCard(this);
        }

        private void PickUp(int zIndex)
        {
            _MouseOffset = MouseService.GetMouseCoordinates() - Position;
            _OriginalPosition = Position;
            _OriginalZIndex = ZIndex;
            ZIndex = zIndex;
        }

        private void SetDown()
        {
            var moved = false;
            _MouseOffset = null;
            object? dropDestination = null;
            var validDrops = GetAllCollisions().Where(c =>

                // Drop on card in lane.
                c is Card card && card.OwnerStack != null && card.Color != Color && card.Value == Value + 1 && !card.IsFaceDown

                // Or drop a king onto an empty lane
                || c is CardLane lane && Value == 13).ToList();

            // If multiple valid drops, pick one closest to the card's center.
            if (validDrops.Skip(1).Any())
            {
                var center = GetCenter();
                dropDestination = validDrops.Select(body => new { body, distance = Vector2.Distance(center, body.GetCenter()) })
                    .OrderBy(c => c.distance)
                    .First();
            }
            else dropDestination = validDrops.FirstOrDefault();

            if (dropDestination != null)
            {
                if (dropDestination is Card card)
                {
                    MoveToNewStack(card.OwnerStack);
                    moved = true;
                }
                else if (dropDestination is CardLane lane)
                {
                    MoveToNewStack(lane);
                    moved = true;
                }
            }

            if (!moved)
            {
                Move(_OriginalPosition.Value);
            }
        }
    }
}

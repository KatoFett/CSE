using GameEngine;
using Raylib_cs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Solitaire
{
    public class MainScene : Scene
    {
        public MainScene() : base("Solitaire", new Vector2(1920, 1080), Color.BLACK, 60)
        {
            _IsInitialized = false;
            Card.SetSize();
            var cardSpacing = new Vector2(30);
            var boardLength = 7 * Card.CardSize.X + 6 * cardSpacing.X;
            var cardX = Size.X / 2 - boardLength / 2;
            var cardY = Card.CardSize.Y + cardSpacing.Y * 2;

            _Background = VideoService.GetTexture("background");
            _Deck = new CardDeck(new Vector2(cardX, cardSpacing.Y));
            _SuitPlaceholders = new Sprite[4]
            {
                new Sprite("card_placeholder")
                {
                    Position = new Vector2(cardX + (Card.CardSize.X + cardSpacing.X) * 3, cardSpacing.Y),
                    Tint = new Color(255, 255, 255, 63)
                },
                new Sprite("card_placeholder")
                {
                    Position = new Vector2(cardX + (Card.CardSize.X + cardSpacing.X) * 4, cardSpacing.Y),
                    Tint = new Color(255, 255, 255, 63)
                },
                new Sprite("card_placeholder")
                {
                    Position = new Vector2(cardX + (Card.CardSize.X + cardSpacing.X) * 5, cardSpacing.Y),
                    Tint = new Color(255, 255, 255, 63)
                },
                new Sprite("card_placeholder")
                {
                    Position = new Vector2(cardX + (Card.CardSize.X + cardSpacing.X) * 6, cardSpacing.Y),
                    Tint = new Color(255, 255, 255, 63)
                },
            };

            // Initialize lanes.
            _Lanes = new CardLane[LANE_COUNT];
            for (int i = 0; i < LANE_COUNT; i++)
            {
                var lane = new CardLane(new Vector2(cardX, cardY));
                _Lanes[i] = lane;
                cardX += Card.CardSize.X + cardSpacing.X;
            }

            StartCoroutine(DealDeck());
        }

        public const int LANE_COUNT = 7;

        private readonly Texture2D _Background;
        private readonly CardLane[] _Lanes;
        private readonly Sprite[] _SuitPlaceholders;

        /// <summary>
        /// Gets whether the scene is initialized.
        /// </summary>
        public bool IsInitialized => _IsInitialized;
        private bool _IsInitialized;

        /// <summary>
        /// Gets the deck of cards in the top-left corner.
        /// </summary>
        public CardDeck Deck => _Deck;
        private readonly CardDeck _Deck;

        public override void Update()
        {
            Raylib.DrawTexture(_Background, 0, 0, Color.GREEN);
            base.Update();
        }

        IEnumerator DealDeck()
        {
            // Each lane gets i + 1 cards.
            for (int col = 0; col < LANE_COUNT; col++)
            {
                for (int row = 0; row < LANE_COUNT; row++)
                {
                    if (row >= col)
                    {
                        var card = _Deck.DrawCard();
                        _Lanes[row].AddCard(card, row == col);
                        yield return new WaitForSeconds(0.05f);
                    }
                }
            }

            _IsInitialized = true;
        }
    }
}

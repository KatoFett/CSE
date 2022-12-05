using GameEngine;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Solitaire
{
    public class MainScene : Scene
    {
        public MainScene() : base("Solitaire", new Vector2(1920, 1080), Color.BLACK, 60)
        {
            var cardTexture = GameEngine.Services.VideoService.GetTexture("card_back");
            var cardSize = new Vector2(cardTexture.width, cardTexture.height);
            var cardSpacing = new Vector2(30);
            var laneCount = 7;
            var boardLength = 7 * cardSize.X + 6 * cardSpacing.X;
            var cardX = Size.X / 2 - boardLength / 2;
            var cardY = cardSize.Y + cardSpacing.Y * 2;

            _Background = GameEngine.Services.VideoService.GetTexture("background");
            _Deck = new Card[52];
            _BinPlaceholder = new Sprite("card_back")
            {
                Position = new Vector2(cardX, cardSpacing.Y),
            };
            _SuitPlaceholders = new Sprite[4]
            {
                new Sprite("card_placeholder")
                {
                    Position = new Vector2(cardX + (cardSize.X + cardSpacing.X) * 3, cardSpacing.Y),
                    Tint = new Color(255, 255, 255, 63)
                },
                new Sprite("card_placeholder")
                {
                    Position = new Vector2(cardX + (cardSize.X + cardSpacing.X) * 4, cardSpacing.Y),
                    Tint = new Color(255, 255, 255, 63)
                },
                new Sprite("card_placeholder")
                {
                    Position = new Vector2(cardX + (cardSize.X + cardSpacing.X) * 5, cardSpacing.Y),
                    Tint = new Color(255, 255, 255, 63)
                },
                new Sprite("card_placeholder")
                {
                    Position = new Vector2(cardX + (cardSize.X + cardSpacing.X) * 6, cardSpacing.Y),
                    Tint = new Color(255, 255, 255, 63)
                },
            };

            for (int s = 0; s < 4; s++)
            {
                for (int i = 0; i < 13; i++)
                {
                    var card = new Card((Card.CardSuit)s, i + 1)
                    {
                        Position = new Vector2(cardX, cardSpacing.Y),
                        IsVisible = false,
                    };
                    _Deck[s * 13 + i] = card;
                }
            }


            _Lanes = new List<Card>[laneCount];
            for (int i = 0; i < laneCount; i++)
            {
                var lane = new List<Card>();
                _Lanes[i] = lane;
                for (int n = 0; n < i + 1; n++)
                {
                    var card = GetRandomCard();
                    card.Lane = lane;
                    card.Position = new Vector2(cardX, cardY + (cardSpacing.Y * n));
                    card.ZIndex = n;
                    card.IsVisible = true;
                    lane.Add(card);
                    if (n == i)
                        card.IsFaceDown = false;
                }
                cardX += cardSize.X + cardSpacing.X;
            }
        }

        private readonly Texture2D _Background;
        private readonly Card[] _Deck;
        private readonly List<Card>[] _Lanes;
        private readonly Sprite _BinPlaceholder;
        private readonly Sprite[] _SuitPlaceholders;

        public override void Update()
        {
            Raylib.DrawTexture(_Background, 0, 0, Color.GREEN);
            base.Update();
        }

        private Card GetRandomCard()
        {
            var cards = _Deck.Where(c => c.Lane == null).ToList();
            var rand = Random.Shared.Next(0, cards.Count);
            return cards[rand];
        }
    }
}

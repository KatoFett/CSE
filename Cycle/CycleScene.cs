using GameEngine;
using GameEngine.Services;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Cycle
{
    /// <summary>
    /// The main scene.
    /// </summary>
    public class CycleScene : Scene
    {
        public CycleScene() : base("Cycle", new Vector2(640, 480), Color.BLACK, 60)
        {
            Bike1 = new();
            Bike2 = new();

            Player1Label = new TextObject("Player 1: 0")
            {
                FontColor = Bike1.Color,
                Position = new Vector2(20f, 10f),
                VerticalAlignment = TextObject.Justification.Top,
                FontSize = 30
            };

            Player2Label = new TextObject("Player 2: 0")
            {
                FontColor = Bike2.Color,
                Position = new Vector2(Size.X - 20f, 10f),
                HorizontalAlignment = TextObject.Alignment.Right,
                VerticalAlignment = TextObject.Justification.Top,
                FontSize = 30
            };

            StartLabel = new TextObject("Press Space to Start")
            {
                FontColor = Color.WHITE,
                Position = Size / 2,
                HorizontalAlignment = TextObject.Alignment.Center,
                FontSize = 40
            };

            Reset();
        }

        private int Player1Score = 0;
        private int Player2Score = 0;
        private readonly Bike1 Bike1;
        private readonly Bike2 Bike2;
        private readonly TextObject Player1Label;
        private readonly TextObject Player2Label;
        private List<Vector2> Bike1Turns = new();
        private List<Vector2> Bike2Turns = new();
        private readonly TextObject StartLabel;
        private readonly float StartLabelAlphaMin = 63;
        private readonly float StartLabelAlphaMax = 255;
        private readonly float StartLabelGlowLength = 2f; // 2 seconds.
        private float StartLabelGlow;
        private bool StartLabelGlowIncrease = false;


        public List<Rectangle> WallRects { get; } = new();

        private bool _IsGameOver = true;
        public bool IsGameOver => _IsGameOver;

        public override void Update()
        {
            base.Update();

            int gridSize = 15;

            // Draw grid.
            for (int i = gridSize; i < Size.X; i += gridSize)
                Raylib.DrawLine(i, 1, i, Size.Y.ToInt() - 1, new Color(23, 23, 23, 255));
            for (int i = gridSize; i < Size.Y; i += gridSize)
                Raylib.DrawLine(1, i, Size.X.ToInt() - 1, i, new Color(23, 23, 23, 255));

            // Draw borders.
            VideoService.DrawRectangle(Size, Vector2.Zero, Color.BLUE);

            // Draw archived bike walls.
            for (int i = 0; i < Bike1Turns.Count - 1; i++)
                DrawBikeWall(Bike1, Bike1Turns[i], Bike1Turns[i + 1]);

            for (int i = 0; i < Bike2Turns.Count - 1; i++)
                DrawBikeWall(Bike2, Bike2Turns[i], Bike2Turns[i + 1]);

            // Draw current bike walls.
            DrawBikeWall(Bike1, Bike1Turns.Last(), Bike1.GetCenter());
            DrawBikeWall(Bike2, Bike2Turns.Last(), Bike2.GetCenter());

            Player1Label.Text = $"Player 1: {Player1Score}";
            Player2Label.Text = $"Player 2: {Player2Score}";

            if (_IsGameOver)
            {
                if (Keyboard.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    _IsGameOver = false;
                    StartLabel.IsVisible = false;
                    Reset();
                }

                if (StartLabelGlowIncrease)
                {
                    StartLabelGlow += DeltaTime;
                    if (StartLabelGlow >= StartLabelGlowLength)
                    {
                        StartLabelGlow = StartLabelGlowLength;
                        StartLabelGlowIncrease = false;
                    }
                }
                else
                {
                    StartLabelGlow -= DeltaTime;
                    if (StartLabelGlow < 0)
                    {
                        StartLabelGlow = 0;
                        StartLabelGlowIncrease = true;
                    }
                }

                StartLabel.FontColor = new Color(255, 255, 255, (int)Raymath.Lerp(StartLabelAlphaMin, StartLabelAlphaMax, StartLabelGlow / StartLabelGlowLength));
            }
        }

        /// <summary>
        /// Registers when a bike rotates to remember the wall.
        /// </summary>
        /// <param name="bike">The bike that has turned.</param>
        public void RegisterBikeRotation(Bike bike)
        {
            var turnsList = bike is Bike1 ? Bike1Turns : Bike2Turns;
            var previousTurn = turnsList.Last();
            var nextTurn = bike.GetCenter();
            turnsList.Add(nextTurn);
            WallRects.Add(GetWallRect(previousTurn, nextTurn));
        }

        /// <summary>
        /// Gets a rectangle containing the entire wall.
        /// </summary>
        /// <param name="pos1">The start position of the wall.</param>
        /// <param name="pos2">The end position of the wall.</param>
        /// <returns>A <see cref="Rectangle"/> whose dimensions match the drawn wall.</returns>
        private static Rectangle GetWallRect(Vector2 pos1, Vector2 pos2)
        {
            var rectX = MathF.Min(pos1.X, pos2.X);
            var rectY = MathF.Min(pos1.Y, pos2.Y);
            var rect = new Rectangle(rectX - 1.5f, rectY - 1.5f, MathF.Abs(pos1.X - pos2.X) + 3f, MathF.Abs(pos1.Y - pos2.Y) + 3f);
            return rect;
        }

        /// <summary>
        /// Draws a bike's wall on the screen.
        /// </summary>
        /// <param name="bike">The bike that created the wall.</param>
        /// <param name="pos1">The start position of the wall.</param>
        /// <param name="pos2">The end position of the wall.</param>
        private static void DrawBikeWall(Bike bike, Vector2 pos1, Vector2 pos2)
        {
            Raylib.DrawLineEx(pos1, pos2, 9f, new Color(255, 255, 255, 7));
            Raylib.DrawLineEx(pos1, pos2, 7f, new Color(255, 255, 255, 15));
            Raylib.DrawLineEx(pos1, pos2, 5f, new Color(255, 255, 255, 23));
            Raylib.DrawLineEx(pos1, pos2, 3f, bike.Color);
        }

        /// <summary>
        /// Gets the rectangles for every wall on the screen.
        /// </summary>
        /// <returns>A list containing a <see cref="Rectangle"/> for every wall drawn by the bikes.</returns>
        public List<Rectangle> GetAllWallRects()
        {
            var result = new List<Rectangle>(WallRects);
            result.AddRange(new Rectangle[] { GetWallRect(Bike1Turns.Last(), Bike1.GetCenter()), GetWallRect(Bike2Turns.Last(), Bike2.GetCenter()) });
            return result;
        }

        /// <summary>
        /// Ends the game and gives a point to the other bike.
        /// </summary>
        /// <param name="caller">The bike that crashed.</param>
        public void EndGame(Bike caller)
        {
            _IsGameOver = true;
            StartLabel.IsVisible = true;
            if (caller is Bike1)
                Player2Score++;
            else
                Player1Score++;
        }

        /// <summary>
        /// Resets the game to the original starting position. Score is not affected.
        /// </summary>
        public void Reset()
        {
            StartLabelGlow = StartLabelGlowLength;
            StartLabelGlowIncrease = false;
            Bike1.Position = new Vector2(Size.X / 4, Size.Y / 2);
            Bike2.Position = new Vector2(Size.X * 3 / 4, Size.Y / 2);

            Bike1Turns = new() { Bike1.GetCenter() };
            Bike2Turns = new() { Bike2.GetCenter() };

            WallRects.Clear();

            Bike1.CurrentDirection = Bike2.CurrentDirection = Bike.Direction.Up;
        }
    }
}

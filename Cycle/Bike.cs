using GameEngine;
using Raylib_cs;
using System.Linq;

namespace Cycle
{
    /// <summary>
    /// A bike in the game.
    /// </summary>
    public abstract class Bike : Sprite
    {
        protected Bike() : base("bike")
        {
            Tint = Color;
        }

        public enum Direction
        {
            Right,
            Down,
            Left,
            Up,
        }

        public readonly float Speed = 160f;

        /// <summary>
        /// Gets or sets the bike's current direction of travel.
        /// </summary>
        public Direction CurrentDirection { get; set; } = Direction.Up;

        /// <summary>
        /// Gets the key to travel left.
        /// </summary>
        public abstract KeyboardKey LeftKey { get; }

        /// <summary>
        /// Gets the key to travel right.
        /// </summary>
        public abstract KeyboardKey RightKey { get; }

        /// <summary>
        /// Gets the key to travel up.
        /// </summary>
        public abstract KeyboardKey UpKey { get; }

        /// <summary>
        /// Gets the key to travel down.
        /// </summary>
        public abstract KeyboardKey DownKey { get; }

        /// <summary>
        /// Gets the bike's color.
        /// </summary>
        public abstract Color Color { get; }

        protected internal override void Update()
        {
            var scene = (CycleScene)Scene.ActiveScene;
            if (scene.IsGameOver) return;

            if (Keyboard.IsKeyPressed(LeftKey) && CurrentDirection != Direction.Right)
            {
                CurrentDirection = Direction.Left;
                scene.RegisterBikeRotation(this);
            }
            else if (Keyboard.IsKeyPressed(RightKey) && CurrentDirection != Direction.Left)
            {
                CurrentDirection = Direction.Right;
                scene.RegisterBikeRotation(this);
            }
            else if (Keyboard.IsKeyPressed(UpKey) && CurrentDirection != Direction.Down)
            {
                CurrentDirection = Direction.Up;
                scene.RegisterBikeRotation(this);
            }
            else if (Keyboard.IsKeyPressed(DownKey) && CurrentDirection != Direction.Up)
            {
                CurrentDirection = Direction.Down;
                scene.RegisterBikeRotation(this);
            }

            var rotation = (int)CurrentDirection * 90f;
            var rotationVector = rotation.ToRotationVector();

            // Move bike forward.
            if (!scene.IsGameOver)
                Translate(rotationVector * Speed * Scene.DeltaTime);

            // If bike runs into border.
            if (Position.X <= 0
                || (Position.X + Size.X) >= Scene.ActiveScene.Size.X
                || Position.Y <= 0
                || (Position.Y + Size.Y) >= Scene.ActiveScene.Size.Y)
                scene.EndGame(this);

            // Check for any wall collisions.
            var walls = scene.GetAllWallRects();
            var thisRect = ToRectangle();
            switch (CurrentDirection)
            {
                case Direction.Right:
                    thisRect.x += 6;
                    thisRect.width = 1;
                    break;
                case Direction.Down:
                    thisRect.y += 6;
                    thisRect.height = 1;
                    break;
                case Direction.Left:
                    thisRect.width = 1;
                    break;
                case Direction.Up:
                    thisRect.height = 1;
                    break;
                default:
                    break;
            }

            var hasCollided = walls.Any(w => Raylib.CheckCollisionRecs(thisRect, w));

            if (hasCollided)
                scene.EndGame(this);
        }
    }
}

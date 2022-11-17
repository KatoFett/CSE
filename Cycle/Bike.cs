using GameEngine;
using GameEngine.Services;
using Raylib_cs;
using System.Linq;

namespace Cycle
{
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

        public Direction CurrentDirection { get; set; } = Direction.Up;

        public abstract KeyboardKey LeftKey { get; }
        public abstract KeyboardKey RightKey { get; }
        public abstract KeyboardKey UpKey { get; }
        public abstract KeyboardKey DownKey { get; }
        public abstract Color Color { get; }

        protected internal override void Update()
        {
            var scene = (CycleScene)Scene.ActiveScene;
            if (scene.IsGameOver) return;

            if (KeyboardService.IsKeyPressed(LeftKey) && CurrentDirection != Direction.Right)
            {
                CurrentDirection = Direction.Left;
                scene.RegisterBikeRotation(this);
            }
            else if (KeyboardService.IsKeyPressed(RightKey) && CurrentDirection != Direction.Left)
            {
                CurrentDirection = Direction.Right;
                scene.RegisterBikeRotation(this);
            }
            else if (KeyboardService.IsKeyPressed(UpKey) && CurrentDirection != Direction.Down)
            {
                CurrentDirection = Direction.Up;
                scene.RegisterBikeRotation(this);
            }
            else if (KeyboardService.IsKeyPressed(DownKey) && CurrentDirection != Direction.Up)
            {
                CurrentDirection = Direction.Down;
                scene.RegisterBikeRotation(this);
            }

            var rotation = (int)CurrentDirection * 90f;
            var rotationVector = rotation.ToRotationVector();

            if(!scene.IsGameOver)
                Translate(rotationVector * Speed * Scene.DeltaTime);

            if (Position.X <= 0
                || (Position.X + Size.X) >= Scene.ActiveScene.Size.X
                || Position.Y <= 0
                || (Position.Y + Size.Y) >= Scene.ActiveScene.Size.Y)
                scene.EndGame(this);

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

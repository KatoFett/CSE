using GameEngine;
using GameEngine.Services;
using Raylib_cs;
using System;
using System.Numerics;

namespace Greed
{
    public class Player : Sprite
    {
        internal Player() : base("player")
        {
            Position = new Vector2((Scene.ActiveScene.Size.X - Size.X) / 2f, Scene.ActiveScene.Size.Y - Size.Y - 10f);
        }

        private readonly float _MovementSpeed = 150f; // Pixels per second.

        /// <summary>
        /// Gets or sets the score of the player.
        /// </summary>
        public int Score { get; set; }

        private readonly GameEngine.Sound _GemSound = new("gem") { Volume = 0.5f };
        private readonly GameEngine.Sound _RockSound = new("rock");

        private readonly float _TextColorChangeLength = 0.2f;
        private float _TextColorChangeDelay;
        private bool _ChangeTextColor = false;

        protected internal override void Update()
        {
            var scene = (GreedScene)Scene.ActiveScene;
            var newPosition = Position;
            if (KeyboardService.IsKeyDown(KeyboardKey.KEY_LEFT))
                newPosition.X -= _MovementSpeed * Scene.DeltaTime;
            if (KeyboardService.IsKeyDown(KeyboardKey.KEY_RIGHT))
                newPosition.X += _MovementSpeed * Scene.DeltaTime;

            Position = Vector2.Clamp(newPosition, Vector2.Zero, Scene.ActiveScene.Size - Size);

            if (_ChangeTextColor)
            {
                if (_TextColorChangeDelay >= _TextColorChangeLength)
                {
                    _ChangeTextColor = false;
                    scene.Score.FontColor = Color.WHITE;
                }
                else _TextColorChangeDelay += Scene.DeltaTime;
            }

            scene.Score.Text = $"Score: {Score}";

            foreach (var body in GetAllCollisions())
            {
                if (body is Mineral mineral)
                {
                    _TextColorChangeDelay = 0f;
                    if (mineral.Type == Mineral.MineralType.Gem)
                    {
                        Score += 10;
                        _GemSound.Pitch = Random.Shared.NextSingle(0.85f, 1.5f);
                        AudioService.PlaySound(_GemSound);
                        scene.Score.FontColor = Color.GREEN;
                    }
                    else
                    {
                        Score -= 10;
                        _RockSound.Pitch = Random.Shared.NextSingle(0.75f, 1.15f);
                        AudioService.PlaySound(_RockSound);
                        scene.Score.FontColor = Color.RED;
                    }
                    mineral.Dispose();
                    _ChangeTextColor = true;
                }
            }
        }
    }
}

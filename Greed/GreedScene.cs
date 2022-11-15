using GameEngine;
using Raylib_cs;
using System;
using System.Numerics;

namespace Greed
{
    public class GreedScene : Scene
    {
        public GreedScene() : base("Greed", new Vector2(640, 480), Color.BLACK, 60)
        {
            Score = new TextObject
            {
                Position = new Vector2(10f, 10f),
                FontColor = Color.WHITE,
                FontSize = 40,
                ZIndex = 1,
            };
            Player = new Player();
        }

        public Player Player { get; }

        public TextObject Score { get; }

        private readonly float _GemChance = 0.2f; // 20%
        private readonly float _MinSpawnDelay = 0.0f;
        private readonly float _MaxSpawnDelay = 0.05f;
        private float _SpawnTimer = 0f;
        private float _NextSpawnTime = 0f;

        public override void Update()
        {
            base.Update();
            _SpawnTimer += DeltaTime;
            if (_SpawnTimer > _NextSpawnTime)
            {
                var mineral = new Mineral
                (
                    Random.Shared.NextSingle() < _GemChance
                        ? Mineral.MineralType.Gem 
                        : Mineral.MineralType.Rock
                );
                mineral.Translate(Random.Shared.NextSingle(-mineral.Size.X, Size.X), 0f);
                _SpawnTimer = 0;
                _NextSpawnTime = Random.Shared.NextSingle(_MinSpawnDelay, _MaxSpawnDelay);
            }
        }
    }
}

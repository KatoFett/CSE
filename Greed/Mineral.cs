using GameEngine;
using System;
using Raylib_cs;

namespace Greed
{
    /// <summary>
    /// A collectible artifact that falls on the screen.
    /// </summary>
    public class Mineral : Sprite
    {
        public Mineral(MineralType mineralType) : base(GetRandomMineralTexture(mineralType))
        {
            Type = mineralType;
            if (mineralType == MineralType.Gem)
                Tint = GemColors[Random.Shared.Next(0, GemColors.Length)];
            else
                Tint = RockColors[Random.Shared.Next(0, RockColors.Length)];
        }

        public enum MineralType
        {
            Gem,
            Rock
        }

        private readonly float _FallSpeed = 100f; // Pixels per second.

        /// <summary>
        /// Gets the type of mineral.
        /// </summary>
        public MineralType Type { get; }

        /// <summary>
        /// Gets a list of all possible gem colors.
        /// </summary>
        public Color[] GemColors { get; } = new Color[] { Color.BLUE, Color.SKYBLUE, Color.VIOLET, Color.RED, Color.GREEN, Color.YELLOW };

        /// <summary>
        /// Gets a list of all possible gem colors.
        /// </summary>
        public Color[] RockColors { get; } = new Color[] { Color.GRAY, Color.BROWN, Color.BEIGE, Color.DARKBROWN, Color.DARKGRAY };

        protected internal override void Update()
        {
            Translate(0f, _FallSpeed * Scene.DeltaTime);
            if (Position.Y >= Scene.ActiveScene.Size.Y)
                Dispose();
        }

        /// <summary>
        /// Gets a random 
        /// </summary>
        /// <returns></returns>
        public static string GetRandomMineralTexture(MineralType mineralType)
        {
            var mineralName = mineralType == MineralType.Gem ? "gem" : "rock";
            var mineralCount = mineralType == MineralType.Gem ? 3 : 2;
            return $"{mineralName}{Random.Shared.Next(1, mineralCount + 1)}";
        }
    }
}

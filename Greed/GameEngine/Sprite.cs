using GameEngine;
using GameEngine.Services;
using Raylib_cs;
using System.Numerics;

namespace GameEngine
{
    /// <summary>
    /// An image or texture with a position.
    /// </summary>
    public class Sprite : PhysicsBody
    {
        /// <summary>
        /// Creates a new instance of an <see cref="Image"/>.
        /// </summary>
        /// <param name="textureName">The name of the texture.</param>
        /// <param name="size">The size of the sprite for physics.</param>
        /// <param name="position">The initial location of the sprite.</param>
        public Sprite(string textureName) : base(Vector2.Zero, Vector2.Zero)
        {
            TextureName = textureName;
            var texture = VideoService.GetTexture(textureName);
            Size = new Vector2(texture.width, texture.height);
        }

        #region GameObject

        protected internal override void Draw(VideoService videoService)
        {
            videoService.DrawImage(this);
        }

        #endregion

        /// <summary>
        /// Gets or sets the name of the sprite's texture.
        /// </summary>
        public string TextureName { get; set; }

        /// <summary>
        /// Gets or sets the tint applied to the sprite's texture.
        /// </summary>
        public Color Tint { get; set; } = Color.WHITE;
    }
}

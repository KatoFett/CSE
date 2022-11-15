namespace GameEngine.Services
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
        public Sprite(string textureName, Vector2D size, Vector2D position) : base(size, position)
        {
            TextureName = textureName;
        }

        #region GameObject

        protected internal override void Draw(VideoService videoService)
        {
            videoService.DrawImage(this);
        }

        #endregion

        /// <summary>
        /// Gets or sets the name of the Sprite's texture.
        /// </summary>
        public string TextureName { get; set; }
    }
}

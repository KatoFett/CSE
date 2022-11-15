namespace GameEngine
{
    /// <summary>
    /// Represents a coordinate in 2D space.
    /// </summary>
    public struct Vector2D
    {
        /// <summary>
        /// Creates a new instance of a <see cref="Vector2D"/> with X- and Y-components of zero.
        /// </summary>
        public Vector2D()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="Vector2D"/> with the specified <paramref name="x"/> and <paramref name="y"/> coordinates.
        /// </summary>
        /// <param name="x">The X-component.</param>
        /// <param name="y">The Y-component.</param>
        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets or sets the X-component of the <see cref="Vector2D"/>.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y-component of the <see cref="Vector2D"/>.
        /// </summary>
        public int Y { get; set; }
    }
}

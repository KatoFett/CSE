using Raylib_cs;

namespace GameEngine
{
    /// <summary>
    /// An object that takes up space in 2D.
    /// </summary>
    public abstract class PhysicsBody : GameObject
    {
        /// <summary>
        /// Creates a new instance of a <see cref="PhysicsBody".
        /// </summary>
        /// <param name="position">The initial position</param>
        /// <param name="size">The initial size</param>
        public PhysicsBody(Vector2D position, Vector2D size)
        {
            Position = position;
            Size = size;
        }

        /// <summary>
        /// Gets or sets the body's size.
        /// </summary>
        public Vector2D Size { get; set; }

        /// <summary>
        /// Gets a <see cref="Rectangle"/> representing the size and position of the body.
        /// </summary>
        public Rectangle ToRectangle()
        {
            return new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
        }

        /// <summary>
        /// Gets whether the body is colliding with another <see cref="PhysicsBody"/>.
        /// </summary>
        /// <param name="body">The other <see cref="PhysicsBody"/> to check.</param>
        /// <returns>A boolean value indicating whether the bodies are colliding.</returns>
        public bool IsCollidingWith(PhysicsBody body)
        {
            Rectangle rect1 = ToRectangle();
            Rectangle rect2 = body.ToRectangle();
            return Raylib.CheckCollisionRecs(rect1, rect2);
        }
    }
}

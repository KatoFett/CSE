using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

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
        /// <param name="position">The initial position.</param>
        /// <param name="size">The initial size.</param>
        public PhysicsBody(Vector2 size, Vector2 position)
        {
            Size = size;
            Position = position;
        }

        /// <summary>
        /// Gets or sets the body's size in pixels relative to the top-left corner.
        /// </summary>
        public Vector2 Size { get; set; }

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

        /// <summary>
        /// Gets all bodies that are currently colliding with this <see cref="PhysicsBody"/>.
        /// </summary>
        /// <returns>A list of all bodies in collision with this <see cref="PhysicsBody"/>.</returns>
        public List<PhysicsBody> GetAllCollisions()
        {
            var retval = new List<PhysicsBody>();
            foreach (var gameObject in Scene.ActiveScene.GameObjects)
            {
                if(gameObject != this && gameObject is PhysicsBody body && IsCollidingWith(body))
                    retval.Add(body);
            }
            return retval;
        }
    }
}

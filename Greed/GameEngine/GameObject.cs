using GameEngine.Services;
using System;

namespace GameEngine
{
    /// <summary>
    /// An object which interacts with the game engine.
    /// </summary>
    public abstract class GameObject : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Creates a new instance of a <see cref="GameObject"/>.
        /// </summary>
        public GameObject()
        {
            Scene.ActiveScene.GameObjects.Add(this);
        }

        ~GameObject()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Scene.ActiveScene.GameObjects.Remove(this);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Gets or sets the body's position.
        /// </summary>
        public Vector2D Position { get; set; }

        /// <summary>
        /// Called one per frame.
        /// </summary>
        protected internal virtual void Update() { }

        /// <summary>
        /// Draws this <see cref="GameObject"/> onto the screen.
        /// </summary>
        /// <param name="videoService"></param>
        protected internal abstract void Draw(VideoService videoService);
    }
}

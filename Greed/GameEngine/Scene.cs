using GameEngine.Services;
using Raylib_cs;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// A 2D game utilizing the engine.
    /// </summary>
    public abstract class Scene
    {
        public Scene(string title, int width, int height, Color background, int fps)
        {
            if (_ActiveScene != null)
                throw new InvalidOperationException("Cannot instantiate multiple scenes.");

            _ActiveScene = this;
            VideoService = new VideoService(title, width, height, background, fps);
        }

        public static Scene ActiveScene => _ActiveScene ?? throw new Exception("No active scene exists.");
        private static Scene? _ActiveScene;

        protected VideoService VideoService { get; }

        /// <summary>
        /// Gets all GameObjects currently in the scene.
        /// </summary>
        internal List<GameObject> GameObjects { get; } = new();

        /// <summary>
        /// Runs the game.
        /// </summary>
        public void Run()
        {
            VideoService.Initialize();
            while (VideoService.IsWindowOpen())
            {
                RenderFrame();
            }
        }

        /// <summary>
        /// Processes and renders a frame to the screen.
        /// </summary>
        public void RenderFrame()
        {
            VideoService.BeginFrame();
            foreach (var gameObj in GameObjects)
            {
                gameObj.Update();
                gameObj.Draw(VideoService);
            }
        }
    }
}

using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

namespace GameEngine.Services
{
#pragma warning disable CA1822 // Mark members as static
    public class VideoService
    {
        private Color _Background;
        private readonly int _Height;
        private readonly int _Width;
        private readonly string _Title;
        private readonly Dictionary<string, Texture2D> _Textures = new();
        private readonly int _FPS;

        public VideoService(string title, int width, int height, Color backgroundColor, int fps)
        {
            _Title = title;
            _Width = width;
            _Height = height;
            _Background = backgroundColor;
            _FPS = fps;
        }

        #region Window Management

        /// <summary>
        /// Initializes the window.
        /// </summary>
        public void Initialize()
        {
            Raylib.InitWindow(_Width, _Height, _Title);
            Raylib.SetTargetFPS(_FPS);
        }

        /// <summary>
        /// Gets whether the window is open.
        /// </summary>
        public bool IsWindowOpen()
        {
            return !Raylib.WindowShouldClose();
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        public void Close()
        {
            Raylib.CloseWindow();
        }

        #endregion

        #region Frame Management

        /// <summary>
        /// Clears the window and begins drawing a new frame.
        /// </summary>
        public void BeginFrame()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(_Background);
        }

        /// <summary>
        /// Ends the frame drawing process.
        /// </summary>
        public void EndFrame()
        {
            Raylib.EndDrawing();
        }

        #endregion

        #region Images

        /// <summary>
        /// Loads all images in the given <paramref name="directory"/> into memory.
        /// </summary>
        /// <param name="directory">The directory in which the images are stored.</param>
        public void LoadImages(string directory)
        {
            string[] filters = new string[] { "*.png", "*.gif", "*.jpg", "*.jpeg", "*.bmp" };
            List<string> filepaths = FileService.GetAllFilePaths(directory, filters);
            foreach (string filepath in filepaths)
            {
                var texture = Raylib.LoadTexture(filepath);
                _Textures[filepath] = texture;
            }
        }

        /// <summary>
        /// Unloads all images from memory.
        /// </summary>
        public void UnloadImages()
        {
            foreach (string key in _Textures.Keys)
            {
                var texture = _Textures[key];
                Raylib.UnloadTexture(texture);
            }
        }

        /// <summary>
        /// Draws an image on the screen
        /// </summary>
        /// <param name="image">The image to draw.</param>
        /// <param name="position">The position of the image's lower-left corner.</param>
        public void DrawImage(Sprite sprite)
        {
            if (!_Textures.ContainsKey(sprite.TextureName))
                throw new KeyNotFoundException($"Unable to find a texture with the name '${sprite.TextureName}'.");

            var texture = _Textures[sprite.TextureName];
            Raylib.DrawTexture(texture, sprite.Position.X, sprite.Position.Y, Color.WHITE);
        }

        /// <summary>
        /// Draws a rectangle on the screen.
        /// </summary>
        /// <param name="size">The size of the rectangle.</param>
        /// <param name="position">The position of the rectangle's top-left corner.</param>
        /// <param name="color">The color of the rectangle.</param>
        /// <param name="filled">Whether the rectangle is filled or hollow.</param>
        public void DrawRectangle(Vector2D size, Vector2D position, Color color, bool filled = false)
        {
            if (filled)
                Raylib.DrawRectangle(position.X, position.Y, size.X, size.Y, color);
            else
                Raylib.DrawRectangleLines(position.X, position.Y, size.X, size.Y, color);
        }

        /// <summary>
        /// Draws a circle on the screen.
        /// </summary>
        /// <param name="position">The position of the circle's center.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="color">The color of the circlle.</param>
        /// <param name="filled">Whether the circle is filled or hollow.</param>
        public void DrawCircle(Vector2D position, float radius, Color color, bool filled = false)
        {
            if (filled)
                Raylib.DrawCircle(position.X, position.Y, radius, color);
            else
                Raylib.DrawCircleLines(position.X, position.Y, radius, color);
        }

        #endregion

        #region Text

        /// <summary>
        /// Draws text on the screen.
        /// </summary>
        /// <param name="text">The text object to be drawn.</param>
        /// <param name="position">The screen position to draw the text at.</param>
        public void DrawText(TextObject text)
        {
            var offset = text.GetXOffset();
            Vector2 vector = new(text.Position.X + offset, text.Position.Y);
            Raylib.DrawTextEx(text.Font, text.Text, vector, text.FontSize, text.FontSpacing, text.Color);
        }

        #endregion
    }
#pragma warning restore CA1822 // Mark members as static
}
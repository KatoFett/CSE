using Raylib_cs;

namespace GameEngine.Services
{
    /// <summary>
    /// Service for interacting with the mouse.
    /// </summary>
    public static class MouseService
    {
        /// <summary>
        /// Gets the coordinates of the mouse cursor.
        /// </summary>
        public static Vector2D GetMouseCoordinates()
        {
            int x = Raylib.GetMouseX();
            int y = Raylib.GetMouseY();
            return new Vector2D(x, y);
        }

        /// <summary>
        /// Gets whether a mouse button is currently held down.
        /// </summary>
        public static bool IsButtonDown(MouseButton button)
        {
            return Raylib.IsMouseButtonDown(button);
        }

        /// <summary>
        /// Gets whether a mouse button has been pressed.
        /// </summary>
        public static bool IsButtonPressed(MouseButton button)
        {
            return Raylib.IsMouseButtonPressed(button);
        }

        /// <summary>
        /// Gets whether a mouse button is currently being released.
        /// </summary>
        public static bool IsButtonReleased(MouseButton button)
        {
            return Raylib.IsMouseButtonReleased(button);
        }

        /// <summary>
        /// Gets whether a mouse button is currently up.
        /// </summary>
        public static bool IsButtonUp(MouseButton button)
        {
            return Raylib.IsMouseButtonUp(button);
        }
    }
}
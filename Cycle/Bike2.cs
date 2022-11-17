using GameEngine.Services;
using Raylib_cs;

namespace Cycle
{
    public class Bike2 : Bike
    {
        public override KeyboardKey LeftKey => KeyboardKey.KEY_J;
        public override KeyboardKey RightKey => KeyboardKey.KEY_L;
        public override KeyboardKey UpKey => KeyboardKey.KEY_I;
        public override KeyboardKey DownKey => KeyboardKey.KEY_K;
        public override Color Color => Color.GREEN;
    }
}

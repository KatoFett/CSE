using GameEngine.Services;
using Raylib_cs;

namespace Cycle
{
    public class Bike1 : Bike
    {
        public override KeyboardKey LeftKey => KeyboardKey.KEY_A;
        public override KeyboardKey RightKey => KeyboardKey.KEY_D;
        public override KeyboardKey UpKey => KeyboardKey.KEY_W;
        public override KeyboardKey DownKey => KeyboardKey.KEY_S;
        public override Color Color => Color.RED;
    }
}

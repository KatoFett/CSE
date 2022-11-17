using GameEngine;

namespace Cycle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextObject.DefaultFont = "Kanit-Regular";
            var scene = new CycleScene();
            scene.Run();
        }
    }
}
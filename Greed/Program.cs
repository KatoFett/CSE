using GameEngine;
using GameEngine.Services;
using Raylib_cs;

namespace Greed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scene = new Scene("Greed", 640, 480, Color.BLACK, 60);
            scene.Run();
        }
    }
}
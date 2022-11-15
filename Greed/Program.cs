using GameEngine;
using GameEngine.Services;
using Raylib_cs;

namespace Greed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextObject.DefaultFont = "Kanit-Regular";
            var scene = new GreedScene();
            scene.Run();
        }
    }
}
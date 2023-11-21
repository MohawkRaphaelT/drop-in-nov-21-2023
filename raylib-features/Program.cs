using Raylib_cs;
using System.Numerics;
//using static Raylib_cs.Raylib;

namespace raylib_features
{
    internal class Program
    {
        // If you need variables in the Program class (outside functions), you must mark them as static
        static string title = "Game Title";
        static Wolf[] wolves;

        static void Main(string[] args)
        {
            // Create a window to draw to. The arguments define width and height
            Raylib.InitWindow(800, 600, title);
            // Set the target frames-per-second (FPS)
            Raylib.SetTargetFPS(60);

            // Setup your game. This is a function YOU define.
            Setup();

            // Loop so long as window should not close
            while (!Raylib.WindowShouldClose())
            {
                // Enable drawing to the canvas (window)
                Raylib.BeginDrawing();
                // Clear the canvas with one color
                Raylib.ClearBackground(Color.RAYWHITE);

                // Your game code here. This is a function YOU define.
                Update();

                // Stop drawing to the canvas, begin displaying the frame
                Raylib.EndDrawing();
            }
            // Close the window
            Raylib.CloseWindow();
        }

        static void Setup()
        {
            wolves = new Wolf[5];
            for (int i = 0; i < wolves.Length; i++)
            {
                wolves[i] = new Wolf();

                int x = 100 + i * 100;
                int y = 100;
                wolves[i].position = new Vector2(x, y);
            }
        }

        static void Update()
        {
            for (int i = 0; i < wolves.Length; i++)
            {
                wolves[i].Move();
                wolves[i].KeepInScreenBounds();
                wolves[i].Draw();
            }
        }


    }
}
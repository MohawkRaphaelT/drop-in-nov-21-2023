using Raylib_cs;
using System.Numerics;
//using static Raylib_cs.Raylib;

namespace raylib_features
{
    internal class Program
    {
        // If you need variables in the Program class (outside functions), you must mark them as static
        static string title = "Game Title";
        //static Wolf[] wolves;
        static Level[] levels;
        static int currentLevelIndex = 0;

        static void Main(string[] args)
        {
            // Create a window to draw to. The arguments define width and height
            Raylib.InitWindow(800, 600, title);
            // Set the target frames-per-second (FPS)
            Raylib.SetTargetFPS(60);
            //Raylib.InitAudioDevice();

            // Setup your game. This is a function YOU define.
            Setup();

            // Loop so long as window should not close
            while (!Raylib.WindowShouldClose())
            {
                // Enable drawing to the canvas (window)
                Raylib.BeginDrawing();              

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
            levels = new Level[]
            {
                Level.CreateLevel1(),
                Level.CreateLevel2(),
            };
        }

        static void Update()
        {
            Raylib.ClearBackground(levels[currentLevelIndex].color); 
            levels[currentLevelIndex].UpdateWolves();

            DrawTime();

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                currentLevelIndex++;
                currentLevelIndex %= levels.Length;
                levels[currentLevelIndex].Reset();
            }
        }

        static void DrawTime()
        {
            double timeToPlay = 10 * 60; // 10 minutes
            double timeRemaining = timeToPlay - Raylib.GetTime();
            int minutes = (int)(timeRemaining / 60);
            int seconds = (int)(timeRemaining % 60);
            Raylib.DrawText($"{minutes}:{seconds:00}", 25, 25, 32, Color.BLACK);

            if (Raylib.GetTime() > timeToPlay)
            {
                // time is up
            }
        }

    }
}
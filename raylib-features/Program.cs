using Raylib_cs;
//using static Raylib_cs.Raylib;

namespace raylib_features
{
    internal class Program
    {
        // If you need variables in the Program class (outside functions), you must mark them as static
        static string title = "Game Title";
        static Texture2D wolf;

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
            wolf = LoadTexture2D("howl.png");
        }

        static void Update()
        {
            Raylib.DrawTexture(wolf, 100, 100, Color.WHITE);
        }

        static Texture2D LoadTexture2D(string fileName)
        {
            Image image = Raylib.LoadImage($"../../../../resources/textures/{fileName}");
            Texture2D texture = Raylib.LoadTextureFromImage(image);
            return texture;
        }
    }
}
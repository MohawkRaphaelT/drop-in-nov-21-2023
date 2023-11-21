using Raylib_cs;
using System.Numerics;

namespace raylib_features
{
    public class Wolf
    {
        public Texture2D texture;
        public Vector2 position;


        public Wolf()
        {
            texture = LoadTexture2D("howl.png");
        }

        public void Draw()
        {
            Raylib.DrawTextureV(texture, position, Color.WHITE);

            Color boxColor = Color.RED;
            Vector2 topLeft = new Vector2(position.X, position.Y);
            Vector2 bottomLeft = new Vector2(position.X, position.Y + texture.Height);
            Vector2 bottomRight = new Vector2(position.X + texture.Width, position.Y + texture.Height);
            Vector2 topRight = new Vector2(position.X + texture.Width, position.Y);
            Raylib.DrawLineV(topLeft, bottomLeft, boxColor);
            Raylib.DrawLineV(bottomLeft, bottomRight, boxColor);
            Raylib.DrawLineV(bottomRight, topRight, boxColor);
            Raylib.DrawLineV(topRight, topLeft, boxColor);
        }

        Texture2D LoadTexture2D(string fileName)
        {
            Image image = Raylib.LoadImage($"../../../../resources/textures/{fileName}");
            Texture2D texture = Raylib.LoadTextureFromImage(image);
            return texture;
        }
    }
}

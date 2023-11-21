using Raylib_cs;
using System.Numerics;

namespace raylib_features
{
    public class Wolf
    {
        public Sound collideSound;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 direction;
        public float speedInPixels;


        public Wolf()
        {
            // Load assets
            collideSound = LoadSound("coin.wav");
            texture = LoadTexture2D("howl.png");
            
            // Set random values
            Random random = new Random();
            // Set random direction
            float randomAngle = random.NextSingle() * MathF.Tau;
            direction = new Vector2(MathF.Cos(randomAngle), MathF.Sin(randomAngle));
            // Set random speed between 100-300
            float percentage = random.NextSingle();
            speedInPixels = 100 + 200 * percentage;
        }

        public void Draw()
        {
            Raylib.DrawTextureV(texture, position, Color.WHITE);

            Color boxColor = Color.BLANK;
            Vector2 topLeft = new Vector2(position.X, position.Y);
            Vector2 bottomLeft = new Vector2(position.X, position.Y + texture.Height);
            Vector2 bottomRight = new Vector2(position.X + texture.Width, position.Y + texture.Height);
            Vector2 topRight = new Vector2(position.X + texture.Width, position.Y);
            Raylib.DrawLineV(topLeft, bottomLeft, boxColor);
            Raylib.DrawLineV(bottomLeft, bottomRight, boxColor);
            Raylib.DrawLineV(bottomRight, topRight, boxColor);
            Raylib.DrawLineV(topRight, topLeft, boxColor);
        }

        public bool DoesCollide(Wolf wolf)
        {
            // Our rectangles. These can be whatever values we want
            // These might go nicely inside a class.
            Vector2 rectanglePosition1 = position;
            Vector2 rectanglePosition2 = wolf.position;
            Vector2 rectangleSize1 = new Vector2(texture.Width, texture.Height);
            Vector2 rectangleSize2 = new Vector2(wolf.texture.Width, wolf.texture.Height);

            // We need to convert our position and size into edges.
            float leftEdge1 = rectanglePosition1.X;
            float rightEdge1 = rectanglePosition1.X + rectangleSize1.X;
            float topEdge1 = rectanglePosition1.Y;
            float bottomEdge1 = rectanglePosition1.Y + rectangleSize1.Y;

            // We must do this for both.
            // This is a lot of repetition. Functions would simplify this code (maybe in a class?).
            float leftEdge2 = rectanglePosition2.X;
            float rightEdge2 = rectanglePosition2.X + rectangleSize2.X;
            float topEdge2 = rectanglePosition2.Y;
            float bottomEdge2 = rectanglePosition2.Y + rectangleSize2.Y;

            // Finally, we can compare.
            // We check if the right edge of one is inside the left edge of the other, and so on.
            bool doesOverlapLeft = leftEdge1 < rightEdge2;
            bool doesOverlapRight = rightEdge1 > leftEdge2;
            bool doesOverlapTop = topEdge1 < bottomEdge2;
            bool doesOverlapBottom = bottomEdge1 > topEdge2;

            // If just want to know if they overlap, we can combine all these Boolean results.
            // We overlap is any result is true, so we combine here with the Logical OR operator.
            bool doesOverlap = doesOverlapLeft && doesOverlapRight && doesOverlapTop && doesOverlapBottom;
            return doesOverlap;
        }

        public void Collide(Wolf wolf)
        {
            bool doesCollide = DoesCollide(wolf);
            if (doesCollide)
            {
                Vector2 deltaPositionA2B = wolf.position - position;
                Vector2 normal = Vector2.Normalize(deltaPositionA2B);
                direction = -normal;

                // play sound
                Raylib.PlaySound(collideSound);
            }
        }

        public void Collide(Wolf[] wolves)
        {
            foreach (Wolf wolf in wolves)
            {
                bool isSameWolf = wolf == this;
                if (!isSameWolf)
                {
                    Collide(wolf);
                }
            }
        }

        public void KeepInScreenBounds()
        {
            // Variables in our code for our player
            // Notice how the position and size could be anything we want
            Vector2 size = new Vector2(texture.Width, texture.Height);

            // Let's also assume we have screen dimensions available
            float screenWidth = Raylib.GetScreenWidth();
            float screenHeight = Raylib.GetScreenHeight();

            // Compute each side of the player
            float playerLeftEdge = position.X;
            float playerRightEdge = position.X + size.X;
            float playerTopEdge = position.Y;
            float playerBottomEdge = position.Y + size.Y;

            // Check each side and see if player is out-of-bounds
            bool isLeftOfWindow = playerLeftEdge <= 0;            // left check
            bool isRightOfWindow = playerRightEdge >= screenWidth;  // right check
            bool isAboveWindow = playerTopEdge <= 0;            // top check
            bool isBelowWindow = playerBottomEdge >= screenHeight; // bottom check

            if ((isLeftOfWindow && direction.X < 0) ||
                (isRightOfWindow && direction.X > 0))
                direction.X = -direction.X;

            if ((isAboveWindow && direction.Y < 0) ||
                (isBelowWindow && direction.Y > 0))
                direction.Y = -direction.Y;
        }

        public void Move()
        {
            position += direction * speedInPixels * Raylib.GetFrameTime();
        }

        Texture2D LoadTexture2D(string fileName)
        {
            Image image = Raylib.LoadImage($"../../../../resources/textures/{fileName}");
            Texture2D texture = Raylib.LoadTextureFromImage(image);
            return texture;
        }

        Sound LoadSound(string fileName)
        {
            Sound sound = Raylib.LoadSound($"../../../../resources/sounds/{fileName}");
            return sound;
        }
    }
}

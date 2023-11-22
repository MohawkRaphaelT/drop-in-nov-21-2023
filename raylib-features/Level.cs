using Raylib_cs;
using System.Numerics;

namespace raylib_features
{
    public class Level
    {
        public Color color;
        public Wolf[] wolves;

        public void PrepareWolves(int numberOfWolves)
        {
            wolves = new Wolf[numberOfWolves];
            for (int i = 0; i < wolves.Length; i++)
            {
                wolves[i] = new Wolf();

                int x = 100 + i * 100;
                int y = 100;
                wolves[i].position = new Vector2(x, y);
            }
        }

        public void UpdateWolves()
        {
            for (int i = 0; i < wolves.Length; i++)
            {
                wolves[i].Move();
                wolves[i].KeepInScreenBounds();
                wolves[i].Collide(wolves);
                wolves[i].Draw();
            }
        }

        public void Reset()
        {
            PrepareWolves(wolves.Length);
        }


        public static Level CreateLevel1()
        {
            Level level1 = new Level();
            level1.PrepareWolves(5);
            level1.color = Color.PINK;

            return level1;
        }
        public static Level CreateLevel2()
        {
            Level level2 = new Level();
            level2.PrepareWolves(100);
            level2.color = Color.SKYBLUE;

            return level2;
        }
    }
}

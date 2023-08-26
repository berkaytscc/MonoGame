using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace rpg
{
    internal class Controller
    {
        public static double timer = 2;
        public static double maxTime = 2;

        private static Random rand = new Random();

        public static void Update(GameTime gameTime, Texture2D spriteSheet)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            if(timer <= 0)
            {
                int side = rand.Next(4);

                switch (side)
                {
                    case 0:     // left side
                        Enemy.Enemies.Add(new Enemy(new Vector2(-500, rand.Next(-500, 2000)), spriteSheet));
                        break;
                    case 1:     // right side
                        Enemy.Enemies.Add(new Enemy(new Vector2(200, rand.Next(-500, 2000)), spriteSheet));
                        break;
                    case 2:     // top side
                        Enemy.Enemies.Add(new Enemy(new Vector2(rand.Next(-500, 2000), -500), spriteSheet));
                        break;
                    case 3:     // buttom side
                        Enemy.Enemies.Add(new Enemy(new Vector2(rand.Next(-500, 2000), 2000), spriteSheet));
                        break;

                }

                timer = maxTime;

                if(maxTime > 0.5)
                {
                    maxTime -= 0.05;
                }
            }
        }
    }
}

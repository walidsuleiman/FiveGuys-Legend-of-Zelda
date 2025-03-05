using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Config;
using FiveGuysFixed.Sprites;

namespace FiveGuysFixed.Enemies
{
    public class Octorok : IEnemy
    {
        private ISprite octorokSprite;
        private double x, y;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private double xAdjust, yAdjust;

        public Octorok(LoadItems items, int x, int y)
        {
            // Make sure your LoadItems has "public ItemData octorok;"
            // Then do:
            // octorokSprite = items.getNewItem(items.octorok);
            // or if you only have Single-Direction Octorok for now, something else.

            // For demonstration, let's say:
            octorokSprite = items.getNewItem(items.stalfos);
            // ^ Replace with items.octorok once you add it to LoadItems!

            this.x = x;
            this.y = y;
            this.currentTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (currentTime < flightTime)
            {
                x += xAdjust;
                y += yAdjust;
            }
            else if (currentTime > flightTime + stillTime)
            {
                currentTime = -1;
                SetAI();
            }

            currentTime++;
            octorokSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            octorokSprite.Draw(spriteBatch, new Vector2((float)x, (float)y), null);
        }

        private void SetAI()
        {
            Random rnd = new Random();
            int decide = rnd.Next(1, 5);
            switch (decide)
            {
                case 1: xAdjust = 0; yAdjust = 1; break;
                case 2: xAdjust = 0; yAdjust = -1; break;
                case 3: xAdjust = 1; yAdjust = 0; break;
                case 4: xAdjust = -1; yAdjust = 0; break;
            }
        }

        public void Attack()
        {
            // Octorok might spit rock projectiles if you wish
        }
    }
}

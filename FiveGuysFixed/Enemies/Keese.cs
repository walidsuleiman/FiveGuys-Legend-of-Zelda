using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Config;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Enemies
{
    public class Keese : IEnemy
    {
        private ISprite keeseSprite;
        private double x, y;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private double xAdjust, yAdjust;

        public double Rad { get { return Math.Max(keeseSprite.Height, keeseSprite.Width); } }

        public Vector2 position { get { return new Vector2((float)x, (float)y); } }

        public Keese(LoadItems items, int x, int y)
        {
            keeseSprite = items.getNewItem(items.keese);
            this.x = x;
            this.y = y;
            currentTime = 0;
            SetAI(); // Initialize movement on creation
        }

        public void Update(GameTime gameTime)
        {
            // Move the Keese
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

            // This is important - this will update the animation frame
            keeseSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            keeseSprite.Draw(spriteBatch, new Vector2((float)x, (float)y), null);
        }

        private void SetAI()
        {
            Random rnd = new Random();
            int decide = rnd.Next(1, 5);

            switch (decide)
            {
                case 1:
                    xAdjust = 0; yAdjust = 1; break;
                case 2:
                    xAdjust = 0; yAdjust = -1; break;
                case 3:
                    xAdjust = 1; yAdjust = 0; break;
                case 4:
                    xAdjust = -1; yAdjust = 0; break;
            }
        }
    }
}
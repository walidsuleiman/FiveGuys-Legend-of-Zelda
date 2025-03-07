using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Config;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Enemies
{
    public class Moblin : IEnemy
    {
        private ISprite currentSprite;
        private ISprite leftMoblinSprite;
        private ISprite rightMoblinSprite;
        private ISprite upMoblinSprite;
        private ISprite downMoblinSprite;

        private double x, y;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private double xAdjust, yAdjust;

        public double Rad { get { return Math.Max(currentSprite.Height, currentSprite.Width); } }

        public Vector2 position { get { return new Vector2((float)x, (float)y); } }

        public Moblin(LoadItems items, int x, int y)
        {
            leftMoblinSprite = items.getNewItem(items.leftMoblin);
            rightMoblinSprite = items.getNewItem(items.rightMoblin);
            upMoblinSprite = items.getNewItem(items.upMoblin);
            downMoblinSprite = items.getNewItem(items.downMoblin);

            currentSprite = downMoblinSprite;
            this.x = x;
            this.y = y;
            currentTime = 0;
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
            currentSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch, new Vector2((float)x, (float)y), null);
        }

        private void SetAI()
        {
            Random rnd = new Random();
            int decide = rnd.Next(1, 5);

            switch (decide)
            {
                case 1:
                    xAdjust = 0; yAdjust = 1;
                    currentSprite = downMoblinSprite;
                    break;
                case 2:
                    xAdjust = 0; yAdjust = -1;
                    currentSprite = upMoblinSprite;
                    break;
                case 3:
                    xAdjust = 1; yAdjust = 0;
                    currentSprite = rightMoblinSprite;
                    break;
                case 4:
                    xAdjust = -1; yAdjust = 0;
                    currentSprite = leftMoblinSprite;
                    break;
            }
        }
    }
}

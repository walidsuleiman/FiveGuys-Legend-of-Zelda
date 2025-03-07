using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Config;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Enemies
{
    public class Tektike : IEnemy, ICollidable
    {
        private ISprite tektikeSprite;
        private double x, y;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private double xAdjust, yAdjust;

        public double Rad { get { return Math.Max(tektikeSprite.Height, tektikeSprite.Width); } }

        public Vector2 position { get { return new Vector2((float)x, (float)y); } }

        CollisionType ICollidable.type => CollisionType.ENEMY;

        public Tektike(LoadItems items, int x, int y)
        {
            // If you have "public ItemData tektike;" in LoadItems
            // tektikeSprite = items.getNewItem(items.tektike);
            // If not, pick any placeholder for now:
            tektikeSprite = items.getNewItem(items.gel);

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
            tektikeSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            tektikeSprite.Draw(spriteBatch, new Vector2((float)x, (float)y), null);
        }

        private void SetAI()
        {
            var rnd = new Random();
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
            // Tektike might jump or do something else
        }

        public void onCollision(ICollidable a, ICollidable b)
        {
            
        }
    }
}

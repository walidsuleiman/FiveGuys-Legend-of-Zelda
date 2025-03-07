using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Config;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Enemies
{
    public class Stalfos : IEnemy, ICollidable
    {
        private ISprite stalfosSprite;
        private double x, y;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private double xAdjust, yAdjust;

        public double Rad { get { return Math.Max(stalfosSprite.Height, stalfosSprite.Width); } }

        public Vector2 position { get { return new Vector2((float)x, (float)y); } }

        CollisionType ICollidable.type => CollisionType.ENEMY;

        public Stalfos(LoadItems items, int x, int y)
        {
            stalfosSprite = items.getNewItem(items.stalfos);
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
            stalfosSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stalfosSprite.Draw(spriteBatch, new Vector2((float)x, (float)y), null);
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
            // Stalfos might not attack
        }

        public void onCollision(ICollidable a, ICollidable b)
        {
            
        }
    }
}

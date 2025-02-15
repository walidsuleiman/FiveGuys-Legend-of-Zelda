using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;

namespace FiveGuysFixed.Enemies
{
    internal class Keese : IEnemy
    {
        private ISprite keeseSprite;
        private double x, y;
        private int currentTime;
        private const int flightTime = 15;
        private const int stillTime = 30;
        private double xAdjust, yAdjust;
        public Keese(Texture2D texture, int x, int y)
        {
            keeseSprite = new Sprite(texture, 16, 32, 16, 16, frames: 2);
            this.x = x;
            this.y = y;
            currentTime = 0;
        }

        public void setAI()
        {
            //this will be refactored to be data driven
            Random rnd = new Random();
            int decide = rnd.Next(1, 5);
            switch (decide)
            {
                case 1:
                    this.xAdjust = 0;
                    this.yAdjust = 1;
                    break;
                case 2:
                    this.xAdjust = 0;
                    this.yAdjust = -1;
                    break;
                case 3:
                    this.xAdjust = 1;
                    this.yAdjust = 0;
                    break;
                case 4:
                    this.xAdjust = -1;
                    this.yAdjust = 0;
                    break;
            }
        }
        public void Attack()
        {
            //keese do not seem to attack
        }

        public void Draw(SpriteBatch spritebatch)
        {
            keeseSprite.Draw(spritebatch, new System.Numerics.Vector2((float)x, (float)y));
        }

        public void Update(GameTime gametime)
        {
            if (currentTime < flightTime)
            {
                this.x += xAdjust;
                this.y += yAdjust;
            }
            else if (currentTime > flightTime + stillTime)
            {
                currentTime = -1;
                setAI();
            }

            currentTime++;

            keeseSprite.Update(gametime);
        }
    }
}

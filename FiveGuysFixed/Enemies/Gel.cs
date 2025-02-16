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
    //implement for sprint 2
    internal class Gel : IEnemy
    {
        //private IPlayer.playerDirections direction
        private Sprite gelSprite;
        private double x, y;
        private int currentTime;
        private const int flightTime = 15;
        private const int stillTime = 30;
        private double xAdjust, yAdjust;

        public Gel(Texture2D texture, int x, int y)
        {
            gelSprite = new Sprite(texture, 16, 16, 16, 16, frames: 2);  
            this.x = x;
            this.y = y;
            currentTime = 0;
        }

        //probably will be moved out into a singleton class to allow all classes to use
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
            //gels do not attack
        }

        public void Draw(SpriteBatch spritebatch)
        {
            gelSprite.Draw(spritebatch, new System.Numerics.Vector2((float)x, (float)y), null);
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

            gelSprite.Update(gametime);
        }
    }
}

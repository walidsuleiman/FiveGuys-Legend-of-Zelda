using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using FiveGuysFixed.Animation;

namespace FiveGuysFixed.Enemies
{
    internal class Moblin : IEnemy
    {
        private Sprite leftMoblinSprite;
        private Sprite rightMoblinSprite;
        private Sprite upMoblinSprite;
        private Sprite downMoblinSprite;
        private Sprite currentMoblin;

        private double x, y;
        private int currentTime;
        private const int flightTime = 15;
        private const int stillTime = 30;
        private double xAdjust, yAdjust;

        private enum Direction { Up, Down, Left, Right }
        private Direction moblinDirection;

        public Moblin(Texture2D texture, int x, int y)
        {
            // sprite positions in Enemy SpriteSheet
            leftMoblinSprite = new Sprite(texture, 80, 128, 16, 16, frames: 2);
            rightMoblinSprite = new Sprite(texture, 48, 128, 16, 16, frames: 2);
            upMoblinSprite = new Sprite(texture, 112, 128, 16, 16, frames: 2);
            downMoblinSprite = new Sprite(texture, 16, 128, 16, 16, frames: 2);

            moblinDirection = Direction.Down;
            currentMoblin = downMoblinSprite;

            this.x = x;
            this.y = y;
            currentTime = 0;
        }

        public void setAI()
        {
            Random rnd = new Random();
            int decide = rnd.Next(1, 5);

            switch (decide)
            {
                case 1:
                    xAdjust = 0;
                    yAdjust = 1;
                    moblinDirection = Direction.Down;
                    currentMoblin = downMoblinSprite;
                    break;
                case 2:
                    xAdjust = 0;
                    yAdjust = -1;
                    moblinDirection = Direction.Up;
                    currentMoblin = upMoblinSprite;
                    break;
                case 3:
                    xAdjust = 1;
                    yAdjust = 0;
                    moblinDirection = Direction.Right;
                    currentMoblin = rightMoblinSprite;
                    break;
                case 4:
                    xAdjust = -1;
                    yAdjust = 0;
                    moblinDirection = Direction.Left;
                    currentMoblin = leftMoblinSprite;
                    break;
            }
        }

        /*
        // Commented out Attack because Boomerang class isn't implemented yet
        public void Attack()
        {
            weapons.Add(new Boomerang(this.items, (int)this.x, (int)this.y, direction));
        }
        */

        public void Draw(SpriteBatch spriteBatch)
        {
            currentMoblin.Draw(spriteBatch, new Vector2((float)x, (float)y));
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
                setAI();
            }

            currentTime++;

            currentMoblin.Update(gameTime);
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }
    }
}

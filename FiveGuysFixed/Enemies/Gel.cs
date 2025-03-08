using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Enemies
{
    public class Gel : Enemy
    {
        private ISprite gelSprite;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private Vector2 velocity;

        public Gel(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            gelSprite = sprite;
            currentTime = 0;
            SetAI();
        }

        public override void Update(GameTime gameTime)
        {
            if (currentTime < flightTime)
            {
                Position += velocity;
            }
            else if (currentTime > flightTime + stillTime)
            {
                currentTime = -1;
                SetAI();
            }

            currentTime++;
            x = (int)Position.X;
            y = (int)Position.Y;
            sprite.Update(gameTime);
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    sprite.Draw(spriteBatch, Position, null);
        //}

        private void SetAI()
        {
            Random rnd = new Random();
            int decide = rnd.Next(1, 5);
            switch (decide)
            {
                case 1: velocity = new Vector2(0, 1); break;
                case 2: velocity = new Vector2(0, -1); break;
                case 3: velocity = new Vector2(1, 0); break;
                case 4: velocity = new Vector2(-1, 0); break;
            }
        }
    }
}

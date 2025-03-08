using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Enemies
{
    public class Stalfos : Enemy
    {
        private ISprite stalfosSprite;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private Vector2 velocity;
        private Random rnd;

        public Stalfos(Vector2 position, Texture2D enemyTexture) : base(position, new EnemySprite(enemyTexture, 16, 96, 16, 2))
        {
            stalfosSprite = new EnemySprite(enemyTexture, 16, 80, 16,16, 2); // Default sprite
            currentTime = 0;
            rnd = new Random();
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
            stalfosSprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            stalfosSprite.Draw(spriteBatch, Position, null);
        }

        private void SetAI()
        {
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

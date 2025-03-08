using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.Enemies
{
    public class Moblin : Enemy
    {
        private ISprite moblinSprite;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private Vector2 velocity;
        private Random rnd;

        public Moblin(Vector2 position, Texture2D enemyTexture) : base(position, new EnemySprite(enemyTexture, 16, 320, 16, 2))
        {
            moblinSprite = new EnemySprite(enemyTexture, 16, 320, 16, 2); // Default down sprite
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
            moblinSprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            moblinSprite.Draw(spriteBatch, Position, null);
        }

        private void SetAI()
        {
            int decide = rnd.Next(1, 5);
            switch (decide)
            {
                case 1:
                    velocity = new Vector2(0, 1);
                    moblinSprite = new EnemySprite(GameState.contentLoader.enemyTexture, 16, 320, 16, 2); // Down
                    break;
                case 2:
                    velocity = new Vector2(0, -1);
                    moblinSprite = new EnemySprite(GameState.contentLoader.enemyTexture, 112, 320, 16, 2); // Up
                    break;
                case 3:
                    velocity = new Vector2(1, 0);
                    moblinSprite = new EnemySprite(GameState.contentLoader.enemyTexture, 48, 320, 16, 2); // Right
                    break;
                case 4:
                    velocity = new Vector2(-1, 0);
                    moblinSprite = new EnemySprite(GameState.contentLoader.enemyTexture, 80, 320, 16, 2); // Left
                    break;
            }
        }
    }
}

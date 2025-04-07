using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Animation;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.Enemies
{
    public class Octorok : Enemy
    {
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private Vector2 velocity;
        private Random rnd;

        public Octorok(Vector2 position, Texture2D enemyTexture)
            : base(position, new EnemySprite(enemyTexture, 16, 304, 16, 16, 2))
        {
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

            // This is important for animation
            sprite.Update(gameTime);
        }

        private void SetAI()
        {
            int decide = rnd.Next(1, 5);
            switch (decide)
            {
                case 1: // Down
                    velocity = new Vector2(0, 1);
                    sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 16, 304, 16, 16, 2);
                    break;
                case 2: // Up
                    velocity = new Vector2(0, -1);
                    sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 112, 304, 16, 16, 2);
                    break;
                case 3: // Right
                    velocity = new Vector2(1, 0);
                    sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 80, 304, 16, 16, 2);
                    break;
                case 4: // Left
                    velocity = new Vector2(-1, 0);
                    sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 48, 304, 16, 16, 2);
                    break;
            }
        }
    }
}
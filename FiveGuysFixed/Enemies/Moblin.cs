using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Animation;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.Enemies
{
    public class Moblin : Enemy
    {
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private Vector2 velocity;
        private Random rnd;

        public Moblin(Vector2 position, Texture2D enemyTexture)
            : base(position, new EnemySprite(enemyTexture, 16, 320, 16, 16, 2))
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
            sprite.Update(gameTime);
        }

        private void SetAI()
        {
            if (DifficultyManager.Instance.ShouldEnemiesTrackPlayer())
            {
                Vector2 direction = EnemyAI.GetMovementDirection(Position);
                float speed = EnemyAI.GetEnemySpeed();

                velocity = direction * speed;

                if (Math.Abs(direction.Y) > Math.Abs(direction.X))
                {
                    if (direction.Y > 0)
                    {
                        // Down
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 16, 320, 16, 16, 2);
                    }
                    else
                    {
                        // Up
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 48, 320, 16, 16, 2);
                    }
                }
                else
                {
                    if (direction.X > 0)
                    {
                        // Right
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 80, 320, 16, 16, 2);
                    }
                    else
                    {
                        // Left
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 112, 320, 16, 16, 2);
                    }
                }
            }
            else
            {
                int decide = rnd.Next(1, 5);
                float speed = EnemyAI.GetEnemySpeed();

                switch (decide)
                {
                    case 1:
                        velocity = new Vector2(0, 1) * speed;
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 16, 320, 16, 16, 2); // down
                        break;
                    case 2:
                        velocity = new Vector2(0, -1) * speed;
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 48, 320, 16, 16, 2); // up
                        break;
                    case 3:
                        velocity = new Vector2(1, 0) * speed;
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 80, 320, 16, 16, 2); // right
                        break;
                    case 4:
                        velocity = new Vector2(-1, 0) * speed;
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 112, 320, 16, 16, 2); // left
                        break;
                }
            }
        }
    }
}
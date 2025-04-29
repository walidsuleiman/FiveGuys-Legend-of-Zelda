using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.Enemies
{
    public class Stalfos : Enemy
    {
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private Vector2 velocity;
        private Random rnd;

        public Stalfos(Vector2 position, Texture2D enemyTexture)
            : base(position, new EnemyCharacterSprite(enemyTexture, 16, 96, 16, 16, 2))
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

                speed *= 0.9f;

                velocity = direction * speed;
            }
            else
            {
                int decide = rnd.Next(1, 5);
                float speed = EnemyAI.GetEnemySpeed();

                speed *= 0.9f;

                switch (decide)
                {
                    case 1: velocity = new Vector2(0, 1) * speed; break;
                    case 2: velocity = new Vector2(0, -1) * speed; break;
                    case 3: velocity = new Vector2(1, 0) * speed; break;
                    case 4: velocity = new Vector2(-1, 0) * speed; break;
                }
            }
        }
    }
}
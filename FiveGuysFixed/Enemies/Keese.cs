using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.Enemies
{
    public class Keese : Enemy
    {
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private Vector2 velocity;
        private Random rnd;

        public Keese(Vector2 position)
            : base(position, EnemySpriteFactory.Instance.CreateKeeseSprite())
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
            float speed = EnemyAI.GetEnemySpeed();
            if (DifficultyManager.Instance.ShouldEnemiesTrackPlayer())
            {
                if (rnd.Next(100) < 20)
                {
                    bool clockwise = rnd.Next(2) == 0;
                    velocity = EnemyAI.GetOrbitDirection(Position, clockwise) * speed * 1.3f;
                }
                else
                {
                    velocity = EnemyAI.GetMovementDirection(Position) * speed * 1.2f;
                }
                return;
            }
            int pattern = rnd.Next(3);
            switch (pattern)
            {
                case 0:
                    velocity = EnemyAI.GetRandomDirection(false) * speed;
                    break;
                case 1:
                    velocity = EnemyAI.GetRandomDirection(true) * speed;
                    break;
                case 2:
                    Vector2 dir = EnemyAI.GetRandomDirection(false);
                    velocity = (dir + new Vector2(-dir.Y, dir.X) * 0.3f) * speed;
                    break;
            }
        }
    }
}
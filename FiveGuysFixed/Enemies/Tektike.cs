using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.Enemies
{
    public class Tektike : Enemy
    {
        private int currentTime;
        private int flightTime, stillTime;
        private Vector2 velocity;
        private readonly Random rnd;

        public Tektike(Vector2 position)
            : base(position, EnemySpriteFactory.Instance.CreateTektikeSprite())
        {
            rnd = new Random();
            flightTime = rnd.Next(10, 25);
            stillTime = rnd.Next(20, 45);
            currentTime = 0;
            SetAI();
        }

        public override void Update(GameTime gameTime)
        {
            if (currentTime < flightTime)
                Position += velocity;
            else if (currentTime > flightTime + stillTime)
            {
                currentTime = -1;
                flightTime = rnd.Next(10, 25);
                stillTime = rnd.Next(20, 45);
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
                if (DifficultyManager.Instance.CurrentDifficulty == GameDifficulty.Hell && rnd.Next(100) < 25)
                    speed *= 1.8f;
                velocity = EnemyAI.GetMovementDirection(Position) * speed;
            }
            else
            {
                switch (rnd.Next(3))
                {
                    case 0: velocity = EnemyAI.GetRandomDirection(false) * speed; break;
                    case 1: velocity = EnemyAI.GetRandomDirection(true) * speed; break;
                    case 2: velocity = EnemyAI.GetOrbitDirection(Position, rnd.Next(2) == 0) * speed; break;
                }
            }
        }
    }
}
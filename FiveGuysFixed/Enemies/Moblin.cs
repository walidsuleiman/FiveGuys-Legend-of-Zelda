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
        private int flightTime, stillTime;
        private Vector2 velocity;
        private readonly Random rnd;

        public Moblin(Vector2 position)
            : base(position, EnemySpriteFactory.Instance.CreateMoblinSprite(Vector2.Zero))
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
                if (rnd.Next(100) < 15)
                    velocity = EnemyAI.GetOrbitDirection(Position, rnd.Next(2) == 0) * speed * 1.2f;
                else
                    velocity = EnemyAI.GetMovementDirection(Position) * speed;
            }
            else
            {
                velocity = EnemyAI.GetRandomDirection(true) * speed;
            }

            sprite = EnemySpriteFactory.Instance.CreateMoblinSprite(velocity);
        }
    }
}
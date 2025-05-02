using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Config;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.Enemies
{
    public class Aquamentus : Enemy
    {
        private readonly ISprite attackSprite;
        private readonly List<IProjectile> projectiles;
        private int currentTime;
        private int flightTime, stillTime;
        private Vector2 velocity;
        private readonly Random rnd = new Random();

        public Aquamentus(Vector2 position, ISprite sprite, ISprite attackSprite, List<IProjectile> projectiles)
            : base(position, sprite, 5f)
        {
            this.health = 10;
            this.attackSprite = attackSprite;
            this.projectiles = projectiles;
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
                if (rnd.Next(3) == 0)
                    velocity = EnemyAI.GetOrbitDirection(Position, rnd.Next(2) == 0) * speed * 1.1f;
                else
                    velocity = EnemyAI.GetMovementDirection(Position) * speed;
                if (rnd.Next(100) < 20) Attack();
            }
            else
            {
                switch (rnd.Next(3))
                {
                    case 0: velocity = EnemyAI.GetRandomDirection(false) * speed; break;
                    case 1: velocity = EnemyAI.GetRandomDirection(true) * speed; break;
                    case 2: velocity = EnemyAI.GetOrbitDirection(Position, rnd.Next(2) == 0) * speed; break;
                }
                if (rnd.Next(100) < 10) Attack();
            }
        }

        private void Attack()
        {
            if (DifficultyManager.Instance.CurrentDifficulty == GameDifficulty.Hell)
            {
                var dirs = new[] { -0.5f, -0.25f, 0, 0.25f, 0.5f };
                foreach (var off in dirs)
                    projectiles.Add(new Fireball(attackSprite.Texture, Position.X, Position.Y + off * 160, new Vector2(-2.5f, off)));
            }
            else
            {
                var offs = new[] { -70f, 0f, 70f };
                foreach (var off in offs)
                    projectiles.Add(new Fireball(attackSprite.Texture, Position.X, Position.Y + off, new Vector2(-2f, 0)));
            }
        }

        public override Rectangle BoundingBox => new Rectangle(
            (int)(Position.X - 32 * 2.5), (int)(Position.Y - 32 * 2.5),
            (int)(32 * scale), (int)(32 * scale));
    }
}
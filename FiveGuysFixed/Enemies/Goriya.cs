using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.Enemies
{
    public class Goriya : Enemy
    {
        private int currentTime;
        private int flightTime, stillTime;
        private Vector2 velocity;
        private readonly List<IProjectile> projectiles;
        private readonly Texture2D boomerangTexture;
        private int attackCooldown, attackCooldownMax;
        private readonly Random rnd;

        public Goriya(Vector2 position, Texture2D boomerangTexture, List<IProjectile> projectiles)
            : base(position, EnemySpriteFactory.Instance.CreateGoriyaSprite(Vector2.Zero))
        {
            this.boomerangTexture = boomerangTexture;
            this.projectiles = projectiles;
            rnd = new Random();
            flightTime = rnd.Next(10, 25);
            stillTime = rnd.Next(20, 45);
            currentTime = 0;

            attackCooldownMax = DifficultyManager.Instance.CurrentDifficulty switch
            {
                GameDifficulty.Easy => 200,
                GameDifficulty.Hard => 100,
                GameDifficulty.Hell => 60,
                _ => 120
            };
            attackCooldown = attackCooldownMax;
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
            if (attackCooldown > 0) attackCooldown--;
            TryAttack();

            x = (int)Position.X;
            y = (int)Position.Y;
            sprite.Update(gameTime);
        }

        private void SetAI()
        {
            float speed = EnemyAI.GetEnemySpeed();

            if (DifficultyManager.Instance.ShouldEnemiesTrackPlayer())
            {
                velocity = EnemyAI.GetMovementDirection(Position) * speed;
                if (rnd.Next(100) < 20)
                    velocity = EnemyAI.GetOrbitDirection(Position, rnd.Next(2) == 0) * speed * 1.3f;
            }
            else
            {
                velocity = EnemyAI.GetRandomDirection(true) * speed;
            }

            sprite = EnemySpriteFactory.Instance.CreateGoriyaSprite(velocity);
        }

        private void TryAttack()
        {
            if (attackCooldown > 0 || boomerangTexture == null || projectiles == null) return;

            int chance = DifficultyManager.Instance.CurrentDifficulty switch
            {
                GameDifficulty.Hard => 2,
                GameDifficulty.Hell => 4,
                _ => 1
            };
            if (rnd.Next(100) >= chance) return;

            Vector2 dir = GameState.PlayerState.position - Position;
            if (dir == Vector2.Zero) dir = new Vector2(1, 0);
            dir.Normalize();
            dir *= DifficultyManager.Instance.CurrentDifficulty switch
            {
                GameDifficulty.Hard => 4f,
                GameDifficulty.Hell => 5f,
                _ => 3f
            };
            projectiles.Add(new Boomerang(boomerangTexture, Position.X, Position.Y, dir, this));
            attackCooldown = attackCooldownMax;
        }
    }
}
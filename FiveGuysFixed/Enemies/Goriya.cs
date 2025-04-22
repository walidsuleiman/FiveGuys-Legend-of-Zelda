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
        private const int flightTime = 15, stillTime = 30;
        private Vector2 velocity;
        private List<IProjectile> projectiles;
        private int attackCooldown;
        private int attackCooldownMax;
        private Random rnd;
        private Texture2D boomerangTexture;

        public Goriya(Vector2 position, Texture2D enemyTexture, Texture2D boomerangTexture, List<IProjectile> projectiles)
            : base(position, new EnemySprite(enemyTexture, 16, 48, 16, 16, 2))
        {
            this.boomerangTexture = boomerangTexture;
            this.projectiles = projectiles;
            currentTime = 0;
            rnd = new Random();

            // Adjust attack frequency based on difficulty
            switch (DifficultyManager.Instance.CurrentDifficulty)
            {
                case GameDifficulty.Easy:
                    attackCooldownMax = 200; // Less frequent attacks
                    break;
                case GameDifficulty.Hard:
                    attackCooldownMax = 100; // More frequent attacks
                    break;
                case GameDifficulty.Hell:
                    attackCooldownMax = 60; // Very frequent attacks
                    break;
                default:
                    attackCooldownMax = 120; // Default
                    break;
            }

            attackCooldown = attackCooldownMax;
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

            // Update attack cooldown
            if (attackCooldown > 0)
                attackCooldown--;

            // Attack probability based on difficulty
            int attackProbability = 1; // Default 1% chance per frame
            if (DifficultyManager.Instance.CurrentDifficulty == GameDifficulty.Hard)
                attackProbability = 2; // 2% chance
            else if (DifficultyManager.Instance.CurrentDifficulty == GameDifficulty.Hell)
                attackProbability = 4; // 4% chance

            if (attackCooldown <= 0 && projectiles != null && rnd.Next(100) < attackProbability)
            {
                Attack();
                attackCooldown = attackCooldownMax;
            }

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
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 16, 48, 16, 16, 2);
                    }
                    else
                    {
                        // Up
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 112, 48, 16, 16, 2);
                    }
                }
                else
                {
                    if (direction.X > 0)
                    {
                        // Right
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 48, 48, 16, 16, 2);
                    }
                    else
                    {
                        // Left
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 80, 48, 16, 16, 2);
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
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 16, 48, 16, 16, 2); // Down
                        break;
                    case 2:
                        velocity = new Vector2(0, -1) * speed;
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 112, 48, 16, 16, 2); // Up
                        break;
                    case 3:
                        velocity = new Vector2(1, 0) * speed;
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 48, 48, 16, 16, 2); // Right
                        break;
                    case 4:
                        velocity = new Vector2(-1, 0) * speed;
                        sprite = new EnemySprite(GameState.contentLoader.enemyTexture, 80, 48, 16, 16, 2); // Left
                        break;
                }
            }
        }

        private void Attack()
        {
            if (projectiles == null || boomerangTexture == null)
                return;

            Vector2 playerPos = GameState.PlayerState.position;

            Vector2 direction = playerPos - Position;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            else
            {
                direction = new Vector2(1, 0);
            }

            float projectileSpeed = 3.0f;
            if (DifficultyManager.Instance.CurrentDifficulty == GameDifficulty.Hard)
                projectileSpeed = 4.0f;
            else if (DifficultyManager.Instance.CurrentDifficulty == GameDifficulty.Hell)
                projectileSpeed = 5.0f;

            direction *= projectileSpeed;

            projectiles.Add(new Boomerang(boomerangTexture, Position.X, Position.Y, direction, this));
        }
    }
}